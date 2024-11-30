namespace Interface.Dto.Conversation;

public record ReplyToDto(
    string ConversationId,
    string ReplyToMessageId);