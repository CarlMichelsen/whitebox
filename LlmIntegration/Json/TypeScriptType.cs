using System.Collections.Immutable;

namespace LLMIntegration.Json;

public record TypeScriptType
{
    public TypeScriptType(
        Keyword keyword,
        Type? derivedFrom,
        string typeName,
        IEnumerable<TypeScriptType>? members = null,
        IEnumerable<TypeScriptType>? generics = null,
        bool isEnumerable = false)
    {
        this.TypeScriptKeyword = keyword;
        this.DerivedFrom = derivedFrom;
        this.TypeName = typeName;
        this.Members = (members ?? []).ToImmutableSortedSet(TypeScriptTypeComparer.Instance);
        this.Generics = (generics ?? []).ToImmutableSortedSet(TypeScriptTypeComparer.Instance);
        this.IsEnumerable = isEnumerable;
    }
    
    public enum Keyword : byte
    {
        /// <summary>
        /// Represents a TypeScript 'type'.
        /// </summary>
        Type,
        
        /// <summary>
        /// Represents a TypeScript 'enum'.
        /// </summary>
        Enum,
        
        /// <summary>
        /// Represents a TypeScript primitive. There is no need to add this when converting types to string.
        /// </summary>
        Primitive,
    }
    
    public Keyword TypeScriptKeyword { get; }
    
    public Type? DerivedFrom { get; }
    
    public string TypeName { get; }
    
    public ImmutableSortedSet<TypeScriptType> Members { get; }
    
    public ImmutableSortedSet<TypeScriptType> Generics { get; }
    
    public bool IsEnumerable { get; }
    
    /// <summary>
    /// Class that is used to sort TypeScriptType deterministically so they can be compared.
    /// </summary>
    private sealed class TypeScriptTypeComparer : IComparer<TypeScriptType>
    {
        public static readonly TypeScriptTypeComparer Instance = new();

        public int Compare(TypeScriptType? x, TypeScriptType? y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x is null)
            {
                return -1;
            }

            if (y is null)
            {
                return 1;
            }

            // First compare by TypeName
            var typeNameComparison = string.Compare(x.TypeName, y.TypeName, StringComparison.Ordinal);
            if (typeNameComparison != 0)
            {
                return typeNameComparison;
            }

            // Then by IsEnumerable
            var enumerableComparison = x.IsEnumerable.CompareTo(y.IsEnumerable);
            if (enumerableComparison != 0)
            {
                return enumerableComparison;
            }

            // Then by member and generic counts
            var memberCountComparison = x.Members.Count.CompareTo(y.Members.Count);
            if (memberCountComparison != 0)
            {
                return memberCountComparison;
            }

            var genericCountComparison = x.Generics.Count.CompareTo(y.Generics.Count);
            if (genericCountComparison != 0)
            {
                return genericCountComparison;
            }

            // If we get here, we need a more complex comparison for content equality
            // We can use a hash code or structural comparison as a fallback
            return x.GetHashCode().CompareTo(y.GetHashCode());
        }
    }
}