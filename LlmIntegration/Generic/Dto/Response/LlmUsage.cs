using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto.Response;

public record LlmUsage(
    [property: JsonPropertyName("inputTokens")] int InputTokens,
    [property: JsonPropertyName("outputTokens")] int OutputTokens);