using LLMIntegration.Util;

namespace LLMIntegration.Model;

public class AnthropicModelGroup
{
    public LlmModel Claude4Sonnet { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 4 Sonnet",
        ModelDescription: "Our most intelligent model",
        ModelIdentifier: "claude-sonnet-4-20250514");
    
    public LlmModel Claude4Opus { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 4 Opus",
        ModelDescription: "Powerful model for highly complex tasks",
        ModelIdentifier: "claude-opus-4-20250514");
    
    public LlmModel Claude35Haiku { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 3.5 Haiku",
        ModelDescription: "Our fastest model",
        ModelIdentifier: "claude-3-5-haiku-latest");
}