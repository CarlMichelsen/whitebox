using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto;

public record GoogleContent(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("parts")] List<GooglePart> Parts);