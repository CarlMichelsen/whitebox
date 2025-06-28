using System.Collections.Immutable;
using System.Text;

namespace LLMIntegration.Json;

public record TypeScriptType
{
    private string? cachedTypeScriptString;
    
    public TypeScriptType(
        Keyword keyword,
        Type? derivedFrom,
        string typeName,
        IEnumerable<TypeScriptTypeMember>? members = null,
        IEnumerable<TypeScriptType>? generics = null,
        bool isEnumerable = false)
    {
        this.TypeScriptKeyword = keyword;
        this.DerivedFrom = derivedFrom;
        this.TypeName = typeName;
        this.Members = (members ?? []).ToImmutableSortedSet(TypeScriptTypeMemberComparer.Instance);
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
    
    public ImmutableSortedSet<TypeScriptTypeMember> Members { get; }
    
    public ImmutableSortedSet<TypeScriptType> Generics { get; }
    
    public bool IsEnumerable { get; init; }

    public override string ToString()
        => this.cachedTypeScriptString ??= this.GenerateTypeScriptString();

    private string GenerateTypeScriptString()
    {
        if (this.TypeScriptKeyword == Keyword.Primitive)
        {
            return this.TypeName;
        }
        
        var delimiter = this.TypeScriptKeyword == Keyword.Enum
            ? ','
            : ';';
        
        var sb = new StringBuilder(KeyWordMapper(this.TypeScriptKeyword));
        sb.Append(' ');
        sb.Append(this.TypeName);
        
        if (this.Generics.Count > 0)
        {
            sb.Append('<');
            foreach (var (generic, index) in this.Generics.Select((g, i) => (g, i)))
            {
                sb.Append(generic.TypeName);
                if (index != this.Generics.Count - 1)
                {
                    sb.Append(',');
                }
            }
            
            sb.Append('>');
        }
        
        if (this.TypeScriptKeyword == Keyword.Type)
        {
            sb.Append(" = ");
        }
        
        sb.Append('{');
        sb.AppendLine();
        
        foreach (var member in this.Members)
        {
            sb.Append('\t');
            sb.Append(member.MemberName);
            sb.Append(':');
            sb.Append(' ');
            sb.Append(member.Type.TypeName);
            if (member.Type.IsEnumerable)
            {
                sb.Append("[]");
            }
            
            sb.Append(delimiter);
            sb.AppendLine();
        }
        
        sb.Append('}');

        return sb.ToString();
    }

    private static string KeyWordMapper(Keyword keyword)
        => keyword switch
        {
            Keyword.Type => "type",
            Keyword.Enum => "enum",
            Keyword.Primitive => throw new ArgumentOutOfRangeException(nameof(keyword), keyword, "Primitive keywords should not be mapped to string"),
            _ => throw new ArgumentOutOfRangeException(nameof(keyword), keyword, null),
        };
}