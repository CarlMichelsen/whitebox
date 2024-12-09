using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto;

public record LlmContent(
    [property: JsonPropertyName("messages")] List<LlmMessage> Messages,
    [property: JsonPropertyName("systemMessage")] string? SystemMessage = default);