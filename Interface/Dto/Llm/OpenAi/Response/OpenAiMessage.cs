using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi.Response;

public record OpenAiMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);