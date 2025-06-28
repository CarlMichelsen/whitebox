namespace LLMIntegration.Json;

/// <summary>
/// Class that is used to sort TypeScriptType deterministically so they can be compared.
/// </summary>
internal sealed class TypeScriptTypeComparer : IComparer<TypeScriptType>
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