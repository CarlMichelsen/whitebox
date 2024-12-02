namespace Interface.Dto.Conversation;

public record ConversationSectionDto(
    string? SelectedMessageId,
    Dictionary<string, MessageDto> Messages);