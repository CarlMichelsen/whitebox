namespace Application.Configuration.Options;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public required string Secret { get; init; }

    public required string Issuer { get; init; }
    
    public required string Audience { get; init; }
}