using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi.Response.Stream;

public record OpenAiChunkDelta(
    [property: JsonPropertyName("role")] string? Role,
    [property: JsonPropertyName("content")] string? Content);