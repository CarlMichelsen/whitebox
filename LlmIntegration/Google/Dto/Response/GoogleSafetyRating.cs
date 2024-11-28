using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto.Response;

public record GoogleSafetyRating(
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("probability")] string Probability);