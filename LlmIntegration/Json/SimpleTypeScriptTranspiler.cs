using System.Reflection;

namespace LLMIntegration.Json;

public class SimpleTypeScriptTranspiler
{
    private static IReadOnlyDictionary<Type, string> TypeMapping { get; } = new Dictionary<Type, string>()
    {
        { typeof(int), "number" },
        { typeof(long), "number" },
        { typeof(float), "number" },
        { typeof(double), "number" },
        { typeof(decimal), "number" },
        { typeof(bool), "boolean" },
        { typeof(string), "string" },
        { typeof(DateTime), "Date" },
        { typeof(DateTimeOffset), "Date" },
        { typeof(Guid), "string" },
        { typeof(object), "any" },
        { typeof(void), "void" },
    };
    
    public List<TypeScriptType> Transpile(List<Type> types, List<TypeScriptType> knownTypes)
    {
        var typesToProcess = new Queue<Type>(types);
        var processedTypes = new HashSet<Type>(knownTypes.Where(t => t.DerivedFrom != null).Select(t => t.DerivedFrom!));
        var result = new List<TypeScriptType>(knownTypes);
        
        while (typesToProcess.Count > 0)
        {
            var type = typesToProcess.Dequeue();
            if (type.IsGenericParameter)
            {
                continue;
            }
            
            // Skip if already processed or primitive
            if (processedTypes.Contains(type) || TypeMapping.ContainsKey(type))
            {
                continue;
            }

            processedTypes.Add(type);
            
            if (type.IsEnum)
            {
                var nameValueList = Enum.GetNames(type).Zip(Enum.GetValues(type).Cast<int>());
                
                var enumValues = nameValueList.Select(nameValue => 
                    new TypeScriptTypeMember(
                        nameValue.First,
                        new TypeScriptType(
                            TypeScriptType.Keyword.Primitive,
                            null,
                            nameValue.Second.ToString())));
                        
                var enumType = new TypeScriptType(
                    TypeScriptType.Keyword.Enum,
                    type,
                    type.Name,
                    enumValues);
                    
                result.Add(enumType);
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var elementType = type.GetGenericArguments()[0];
                typesToProcess.Enqueue(elementType);
            }
            else
            {
                var members = new List<TypeScriptTypeMember>();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(p => p.GetIndexParameters().Length == 0); // Skip indexers
                                    
                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;
                    var typeScriptType = this.GetTypeScriptType(propertyType);
                    
                    // Create a member with the property name but with the correct type
                    members.Add(new TypeScriptTypeMember(
                        property.Name,
                        typeScriptType));
                    
                    // Add property type to processing queue if it's not a primitive
                    if (!TypeMapping.ContainsKey(propertyType) && !processedTypes.Contains(propertyType))
                    {
                        if (IsEnumerable(propertyType))
                        {
                            var elementType = GetEnumerableType(propertyType);
                            var enumerableType = this.GetTypeScriptType(elementType) with { IsEnumerable = true };
                            members.Add(new TypeScriptTypeMember(
                                property.Name,
                                enumerableType));
                            if (!TypeMapping.ContainsKey(elementType) && !processedTypes.Contains(elementType))
                            {
                                typesToProcess.Enqueue(elementType);
                            }
                        }
                        else
                        {
                            typesToProcess.Enqueue(propertyType);
                        }
                    }
                }
                
                var genericArguments = type.IsGenericType ? type.GetGenericArguments().Where(arg => arg.IsGenericParameter) : [];
                var generics = new List<TypeScriptType>();
                
                foreach (var genericArg in genericArguments)
                {
                    var typeScriptGeneric = this.GetTypeScriptType(genericArg);
                    generics.Add(typeScriptGeneric);
                    
                    if (!TypeMapping.ContainsKey(genericArg) && !processedTypes.Contains(genericArg))
                    {
                        if (IsEnumerable(genericArg))
                        {
                            var elementType = GetEnumerableType(genericArg);
                            if (!TypeMapping.ContainsKey(elementType) && !processedTypes.Contains(elementType))
                            {
                                typesToProcess.Enqueue(elementType);
                            }
                        }
                        else
                        {
                            typesToProcess.Enqueue(genericArg);
                        }
                    }
                }
                
                var tsTypeObj = new TypeScriptType(
                    TypeScriptType.Keyword.Type,
                    type,
                    type.Name.Split('`')[0], // Remove generic arity notation
                    members,
                    generics.Count != 0 ? generics : null);
                    
                result.Add(tsTypeObj);
            }
        }
        
        return result;
    }
    
    private static bool IsEnumerable(Type type)
    {
        if (type.IsArray)
        {
            return true;
        }

        return type.IsGenericType && (
            type.GetGenericTypeDefinition() == typeof(List<>) ||
            type.GetGenericTypeDefinition() == typeof(IList<>) ||
            type.GetGenericTypeDefinition() == typeof(ICollection<>) ||
            type.GetGenericTypeDefinition() == typeof(IEnumerable<>));
    }

    private static Type GetEnumerableType(Type type)
        => type.IsArray
            ? type.GetElementType()!
            : type.GetGenericArguments()[0];

    private TypeScriptType GetTypeScriptType(Type type)
    {
        // Handle primitives
        if (TypeMapping.TryGetValue(type, out var primitiveTypeName))
        {
            return new TypeScriptType(TypeScriptType.Keyword.Primitive, type, primitiveTypeName);
        }
        
        // Handle arrays and lists
        if (IsEnumerable(type))
        {
            var elementType = GetEnumerableType(type);
            var innerType = this.GetTypeScriptType(elementType);
            
            return new TypeScriptType(
                innerType.TypeScriptKeyword,
                type,
                innerType.TypeName,
                innerType.Members,
                innerType.Generics,
                true);
        }
        
        // Handle other generic types
        if (type.IsGenericType)
        {
            var genericArgs = type.GetGenericArguments()
                .Select(this.GetTypeScriptType)
                .ToList();
                
            return new TypeScriptType(
                TypeScriptType.Keyword.Type,
                type,
                type.Name.Split('`')[0],
                null,
                genericArgs);
        }
        
        // For enums
        if (type.IsEnum)
        {
            return new TypeScriptType(
                TypeScriptType.Keyword.Enum,
                type,
                type.Name);
        }
        
        // Handle regular class/struct types
        return new TypeScriptType(
            TypeScriptType.Keyword.Type,
            type,
            type.Name);
    }
}