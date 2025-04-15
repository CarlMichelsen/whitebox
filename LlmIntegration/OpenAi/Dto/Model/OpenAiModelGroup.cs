using LLMIntegration.Util;

namespace LLMIntegration.OpenAi.Dto.Model;

public class OpenAiModelGroup
{
    public LlmModel O3Mini { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-o3-mini",
        ModelDescription: "o3-mini is our newest small reasoning model, providing high intelligence at the same cost and latency targets of o1-mini. o3-mini supports key developer features, like Structured Outputs, function calling, and Batch API.",
        ModelIdentifier: "o3-mini",
        MaxCompletionTokens: 16384);

    public LlmModel Gpt41 { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-4.1",
        ModelDescription: "GPT-4.1 is our flagship model for complex tasks. It is well suited for problem solving across domains.",
        ModelIdentifier: "gpt-4.1",
        MaxCompletionTokens: 16384);
    
    public LlmModel Gpt4O { get; } = new(
        Provider: LlmProvider.OpenAi,
        ModelName: "GPT-4o",
        ModelDescription: "Our high-intelligence flagship model for complex, multi-step tasks",
        ModelIdentifier: "gpt-4o",
        MaxCompletionTokens: 16384);
}