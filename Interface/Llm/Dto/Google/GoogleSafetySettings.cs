using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google;

public record GoogleSafetySettings(
    [property: JsonPropertyName("category")] string Category,
    [property: JsonPropertyName("threshold")] string Threshold);