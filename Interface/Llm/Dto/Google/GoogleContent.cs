using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google;

public record GoogleContent(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("parts")] List<GooglePart> Parts);