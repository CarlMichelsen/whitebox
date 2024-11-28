using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto;

public record OpenAiContent(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("text")] string Text);