using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto.Response;

public record OpenAiChoice(
    [property: JsonPropertyName("index")] int Index,
    [property: JsonPropertyName("message")] OpenAiResponseMessage ResponseMessage,
    [property: JsonPropertyName("logprobs")] object? Logprobs,
    [property: JsonPropertyName("finish_reason")] string FinishReason);