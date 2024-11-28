using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto;

public record GooglePrompt(
    [property: JsonIgnore]
    string Model,
    [property: JsonPropertyName("contents")]
    List<GoogleContent> Contents,
    [property: JsonPropertyName("safetySettings")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    List<GoogleSafetySettings>? SafetySettings = null);