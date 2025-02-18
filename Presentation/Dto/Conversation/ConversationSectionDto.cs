using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation;

public record ConversationSectionDto(
    [property: JsonPropertyName("selectedMessageId")] Guid? SelectedMessageId,
    [property: JsonPropertyName("messages")] Dictionary<Guid, MessageDto> Messages);