using LLMIntegration.Util;

namespace LLMIntegration.Model;

public class GoogleModelGroup
{
    public LlmModel Gemini25Pro { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 2.5 Pro",
        ModelDescription: "Enhanced thinking and reasoning, multimodal understanding, advanced coding, and more",
        ModelIdentifier: "gemini-2.5-pro");
    
    public LlmModel Gemini25Flash { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 2.5 Flash",
        ModelDescription: " Adaptive thinking, cost efficiency",
        ModelIdentifier: "gemini-2.5-flash");
    
    public LlmModel Gemini25FlashLite { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 2.5 Flash (Preview)",
        ModelDescription: "Most cost-efficient model supporting high throughput",
        ModelIdentifier: "gemini-2.5-flash-lite-preview-06-17");
}