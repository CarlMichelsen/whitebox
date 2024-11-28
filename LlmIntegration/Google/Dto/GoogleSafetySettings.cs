using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto;

public record GoogleSafetySettings(
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("threshold")] string Threshold);