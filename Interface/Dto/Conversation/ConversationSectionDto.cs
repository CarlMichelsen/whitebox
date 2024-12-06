namespace Interface.Dto.Conversation;

public record ConversationSectionDto(
    Guid? SelectedMessageId,
    Dictionary<Guid, MessageDto> Messages);