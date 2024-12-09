using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto.Response;

public record LlmResponse(
    [property: JsonPropertyName("responseId")] string ResponseId,
    [property: JsonPropertyName("modelIdentifier")] string ModelIdentifier,
    [property: JsonPropertyName("role")] LlmRole Role,
    [property: JsonPropertyName("stopReason")] string StopReason,
    [property: JsonPropertyName("parts")] List<LlmPart> Parts,
    [property: JsonPropertyName("usage")] LlmUsage Usage);