using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto.Response.Stream;

public record OpenAiChunkDelta(
    [property: JsonPropertyName("role")] string? Role,
    [property: JsonPropertyName("content")] string? Content);