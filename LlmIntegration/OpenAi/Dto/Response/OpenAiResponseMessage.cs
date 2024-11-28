using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto.Response;

public record OpenAiResponseMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);