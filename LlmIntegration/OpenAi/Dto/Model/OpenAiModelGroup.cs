using LLMIntegration.Util;

namespace LLMIntegration.OpenAi.Dto.Model;

public class OpenAiModelGroup
{
    public LlmModel Gpt4O { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-4o",
        ModelDescription: "Our high-intelligence flagship model for complex, multi-step tasks",
        ModelIdentifier: "gpt-4o");
    
    public LlmModel Gpt4OMini { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-4o mini",
        ModelDescription: "Our affordable and intelligent small model for fast, lightweight tasks",
        ModelIdentifier: "gpt-4o-mini");
}