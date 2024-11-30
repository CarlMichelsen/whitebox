namespace Interface.Dto.Conversation;

public record AppendConversationDto(
    ReplyToDto? ReplyTo,
    string Text);