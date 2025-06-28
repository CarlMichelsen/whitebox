namespace LLMIntegration.Json;

internal sealed class TypeScriptTypeMemberComparer : IComparer<TypeScriptTypeMember>
{
    public static readonly TypeScriptTypeMemberComparer Instance = new();
    
    public int Compare(TypeScriptTypeMember? x, TypeScriptTypeMember? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (y is null)
        {
            return 1;
        }

        if (x is null)
        {
            return -1;
        }

        return string.Compare(x.MemberName, y.MemberName, StringComparison.Ordinal);
    }
}