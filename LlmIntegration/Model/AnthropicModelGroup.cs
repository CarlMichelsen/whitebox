using LLMIntegration.Util;

namespace LLMIntegration.Model;

public class AnthropicModelGroup
{
    public LlmModel Claude37Sonnet { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 3.7 Sonnet",
        ModelDescription: "Our most intelligent model",
        ModelIdentifier: "claude-3-7-sonnet-latest");
    
    public LlmModel Claude35Haiku { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 3.5 Haiku",
        ModelDescription: "Our fastest model",
        ModelIdentifier: "claude-3-5-haiku-latest");
    
    public LlmModel Claude3Opus { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 3 Opus",
        ModelDescription: "Powerful model for highly complex tasks",
        ModelIdentifier: "claude-3-opus-latest");
}