using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto;

public record LlmPart(
    [property: JsonPropertyName("type")] PartType Type,
    [property: JsonPropertyName("content")] string Content);