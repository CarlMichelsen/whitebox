using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation;

public record ConversationSectionDto(
    [property: JsonPropertyName("selectedMessageId")] Guid? SelectedMessageId,
    [property: JsonPropertyName("messages")] Dictionary<Guid, MessageDto> Messages);