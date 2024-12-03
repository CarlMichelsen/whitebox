namespace Domain.Conversation.Action;

public record AppendConversation(
    ReplyTo? ReplyTo,
    string Text);