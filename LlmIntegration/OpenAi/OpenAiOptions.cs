namespace LLMIntegration.OpenAi;

public class OpenAiOptions
{
    public const string SectionName = "OpenAi";
    
    public required List<string> ApiKeys { get; init; }
    
    public required string ApiEndpoint { get; init; }
}