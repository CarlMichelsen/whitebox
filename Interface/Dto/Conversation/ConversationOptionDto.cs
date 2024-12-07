using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation;

public record ConversationOptionDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("lastAltered")] long LastAltered);