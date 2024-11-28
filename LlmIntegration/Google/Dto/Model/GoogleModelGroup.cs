using LLMIntegration.Util;

namespace LLMIntegration.Google.Dto.Model;

public class GoogleModelGroup
{
    public LlmModel Flash15 { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "1.5 Flash",
        ModelDescription: "Our most balanced multimodal model with great performance for most tasks",
        ModelIdentifier: "gemini-1.5-flash");
    
    public LlmModel Flash15Dash8B { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "1.5 Flash-8B",
        ModelDescription: "Our fastest and most cost-efficient multimodal model with great performance for high-frequency tasks",
        ModelIdentifier: "gemini-1.5-flash-8b");
    
    public LlmModel Pro15 { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "1.5 Pro",
        ModelDescription: "Our best performing multimodal model with features for a wide variety of reasoning tasks",
        ModelIdentifier: "gemini-1.5-pro");
}