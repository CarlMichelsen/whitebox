using LLMIntegration.Util;

namespace LLMIntegration.Model;

public class LegacyModels
{
    public LlmModel Gpt4OMini { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-4o mini",
        ModelDescription: "Our affordable and intelligent small model for fast, lightweight tasks",
        ModelIdentifier: "gpt-4o-mini",
        MaxCompletionTokens: 16384,
        IsLegacy: true);
    
    public LlmModel O3Mini { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-o3-mini",
        ModelDescription: "o3-mini is our newest small reasoning model, providing high intelligence at the same cost and latency targets of o1-mini. o3-mini supports key developer features, like Structured Outputs, function calling, and Batch API.",
        ModelIdentifier: "o3-mini",
        MaxCompletionTokens: 16384,
        IsLegacy: true);

    public LlmModel Claude35Sonnet { get; } = new(
        Provider: LlmProvider.Anthropic,
        ModelName: "Claude 3.5 Sonnet",
        ModelDescription: "Our previous most intelligent model",
        ModelIdentifier: "claude-3-5-sonnet-latest",
        IsLegacy: true);

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

    public LlmModel GrokBeta { get; } = new(
        Provider: LlmProvider.X,
        ModelName: "Grok Beta",
        ModelDescription: "Comparable performance to Grok 2 but with improved efficiency, speed and capabilities.",
        ModelIdentifier: "grok-beta",
        IsLegacy: true);
    
    public LlmModel Grok2 { get; } = new(
        Provider: LlmProvider.X,
        ModelName: "Grok 2",
        ModelDescription: "Grok version 2.",
        ModelIdentifier: "grok-2-1212",
        IsLegacy: true);
}