using LLMIntegration.Util;

namespace LLMIntegration.Model;

public class GoogleModelGroup
{
    public LlmModel Flash20 { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 2.0 Flash",
        ModelDescription: "Gemini 2.0 Flash delivers next-gen features and improved capabilities, including superior speed, native tool use, multimodal generation, and a 1M token context window.",
        ModelIdentifier: "gemini-2.0-flash");
    
    public LlmModel Flash15Dash8B { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 2.0 Flash Lite",
        ModelDescription: "A Gemini 2.0 Flash model optimized for cost efficiency and low latency.",
        ModelIdentifier: "gemini-2.0-flash-lite-preview-02-05");
    
    public LlmModel Flash15 { get; } = new(
        Provider: LlmProvider.Google,
        ModelName: "Gemini 1.5 Flash",
        ModelDescription: "Our most balanced multimodal model with great performance for most tasks",
        ModelIdentifier: "gemini-1.5-flash");
}