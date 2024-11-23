namespace LLMIntegration.Anthropic;

public class AnthropicOptions
{
    public const string SectionName = "Anthropic";
    
    public required List<string> ApiKeys { get; init; }
    
    public required string AnthropicVersion { get; init; }
    
    public required string ApiEndPoint { get; init; }
}