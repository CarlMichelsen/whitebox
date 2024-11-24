using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Google;

public record GoogleSafetySettings(
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("threshold")] string Threshold);