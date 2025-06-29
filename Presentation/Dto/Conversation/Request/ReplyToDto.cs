namespace Presentation.Dto.Conversation.Request;

public record ReplyToDto(
    Guid ConversationId,
    Guid? ReplyToMessageId);