using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto;

public record LlmMessage(
    [property: JsonPropertyName("role")] LlmRole Role,
    [property: JsonPropertyName("parts")] List<LlmPart> Parts);