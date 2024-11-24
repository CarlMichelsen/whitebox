namespace LLMIntegration.Google;

public class GoogleOptions
{
    public const string SectionName = "Google";
    
    public required List<string> ApiKeys { get; init; }
    
    public required string ApiEndpoint { get; init; }
}