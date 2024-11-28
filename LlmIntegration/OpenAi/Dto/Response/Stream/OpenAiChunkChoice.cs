using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto.Response.Stream;

public record OpenAiChunkChoice(
    [property: JsonPropertyName("index")] int Index,
    [property: JsonPropertyName("delta")] OpenAiChunkDelta Delta,
    [property: JsonPropertyName("logprobs")] object? Logprobs,
    [property: JsonPropertyName("finish_reason")] string? FinishReason);