
using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Google;

public record GoogleContent(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("parts")] List<GooglePart> Parts);