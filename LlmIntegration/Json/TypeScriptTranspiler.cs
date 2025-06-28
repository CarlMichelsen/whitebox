using System.Reflection;

namespace LLMIntegration.Json;

public class TypeScriptTranspiler
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
            
            // Skip if already processed or primitive
            if (processedTypes.Contains(type) || TypeMapping.ContainsKey(type))
            {
                continue;
            }

            processedTypes.Add(type);
            
            if (type.IsEnum)
            {
                var enumValues = Enum.GetNames(type).Select(name => 
                    new TypeScriptType(
                        TypeScriptType.Keyword.Primitive, 
                        null, 
                        name));
                        
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
            else if (IsEnumerable(type))
            {
                // TODO: this should be after properties/generics assignment - we need to add an enumerable copy to the member/generics list.
                var elementType = GetEnumerableType(type);
                typesToProcess.Enqueue(elementType);
            }
            else
            {
                var members = new List<TypeScriptType>();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(p => p.GetIndexParameters().Length == 0); // Skip indexers
                                    
                foreach (var property in properties)
                {
                    // TODO: process the actual type recursively here instead of adding to queue - if enumerable make a copy and add it to member directly
                    var propertyType = property.PropertyType;
                    members.Add(new TypeScriptType(
                        TypeScriptType.Keyword.Primitive,
                        propertyType,
                        property.Name,
                        null,
                        null,
                        false));
                    
                    // Add property type to processing queue if it's not a primitive
                    if (!TypeMapping.ContainsKey(propertyType) && !processedTypes.Contains(propertyType))
                    {
                        typesToProcess.Enqueue(propertyType);
                    }
                }
                
                var genericArguments = type.IsGenericType ? type.GetGenericArguments() : [];
                var generics = genericArguments.Select(this.GetTypeScriptType).ToList();
                
                foreach (var genericArg in genericArguments)
                {
                    if (!TypeMapping.ContainsKey(genericArg) && !processedTypes.Contains(genericArg))
                    {
                        typesToProcess.Enqueue(genericArg);
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
        if (type.IsArray || 
            (type.IsGenericType && (
                type.GetGenericTypeDefinition() == typeof(List<>) ||
                type.GetGenericTypeDefinition() == typeof(IList<>) ||
                type.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                type.GetGenericTypeDefinition() == typeof(IEnumerable<>))))
        {
            Type elementType = type.IsArray ? type.GetElementType()! : type.GetGenericArguments()[0];
            var innerType = this.GetTypeScriptType(elementType);
            
            return new TypeScriptType(
                TypeScriptType.Keyword.Primitive,
                type,
                innerType.TypeName,
                null,
                null,
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
        
        // Handle regular class/struct types
        return new TypeScriptType(
            TypeScriptType.Keyword.Type,
            type,
            type.Name);
    }
}