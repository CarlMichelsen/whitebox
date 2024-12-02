namespace Interface.Dto.Conversation.Request;

public record AppendConversationDto(
    ReplyToDto? ReplyTo,
    string Text);