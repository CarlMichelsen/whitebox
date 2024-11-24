using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi.Response.Stream;

public record OpenAiChunkDelta(
    [property: JsonPropertyName("role")] string? Role,
    [property: JsonPropertyName("content")] string? Content);