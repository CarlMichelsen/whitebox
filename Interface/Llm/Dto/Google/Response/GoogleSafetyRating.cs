using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google.Response;

public record GoogleSafetyRating(
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("probability")] string Probability);