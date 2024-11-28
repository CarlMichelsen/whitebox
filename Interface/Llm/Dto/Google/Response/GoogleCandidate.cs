using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google.Response;

public record GoogleCandidate(
    [property: JsonPropertyName("content")] GoogleContent Content,
    [property: JsonPropertyName("finishReason")] string FinishReason,
    [property: JsonPropertyName("safetyRatings")] List<GoogleSafetyRating> SafetyRatings);