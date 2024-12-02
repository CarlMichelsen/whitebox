namespace Interface.Dto.Conversation.Request;

public record ReplyToDto(
    string ConversationId,
    string ReplyToMessageId);