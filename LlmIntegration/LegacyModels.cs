using LLMIntegration.Util;

namespace LLMIntegration;

public class LegacyModels
{
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