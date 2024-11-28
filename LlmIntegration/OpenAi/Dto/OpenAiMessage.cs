using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto;

public record OpenAiMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] List<OpenAiContent> Content);