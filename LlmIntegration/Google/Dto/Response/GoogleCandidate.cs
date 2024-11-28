using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto.Response;

public record GoogleCandidate(
    [property: JsonPropertyName("content")] GoogleContent Content,
    [property: JsonPropertyName("finishReason")] string FinishReason,
    [property: JsonPropertyName("safetyRatings")] List<GoogleSafetyRating> SafetyRatings);