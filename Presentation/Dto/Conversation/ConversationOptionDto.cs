using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation;

public record ConversationOptionDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("summary")] string Summary,
    [property: JsonPropertyName("lastAltered")] long LastAltered);