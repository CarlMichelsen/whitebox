using Database.Entity.Id;

namespace Domain.Conversation.Action;

public record ReplyTo(
    ConversationEntityId ConversationId,
    MessageEntityId ReplyToMessageId);