using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation;

public record MessageContentDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value,
    [property: JsonPropertyName("sortOrder")] int SortOrder);