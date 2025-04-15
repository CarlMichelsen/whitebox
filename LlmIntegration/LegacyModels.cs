using LLMIntegration.Util;

namespace LLMIntegration;

public class LegacyModels
{
    public LlmModel Gpt4OMini { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-4o mini",
        ModelDescription: "Our affordable and intelligent small model for fast, lightweight tasks",
        ModelIdentifier: "gpt-4o-mini",
        MaxCompletionTokens: 16384);

    public LlmModel Claude35Sonnet { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 3.5 Sonnet",
        ModelDescription: "Our previous most intelligent model",
        ModelIdentifier: "claude-3-5-sonnet-latest");

    public LlmModel Flash15Dash8B { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 1.5 Flash-8B",
        ModelDescription: "Our fastest and most cost-efficient multimodal model with great performance for high-frequency tasks",
        ModelIdentifier: "gemini-1.5-flash-8b",
        IsLegacy: true);
    
    public LlmModel Pro15 { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 1.5 Pro",
        ModelDescription: "Our best performing multimodal model with features for a wide variety of reasoning tasks",
        ModelIdentifier: "gemini-1.5-pro",
        IsLegacy: true);
}