namespace LLMIntegration.X;

public class XAiOptions
{
    public const string SectionName = "XAi";
    
    public required List<string> ApiKeys { get; init; }
    
    public required string ApiEndpoint { get; init; }
}