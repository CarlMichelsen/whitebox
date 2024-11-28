using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi.Response;

public record OpenAiResponseMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);