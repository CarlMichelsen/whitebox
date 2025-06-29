using Database.Entity;
using Database.Entity.Id;
using Domain.Conversation.Action;

namespace Presentation.Repository;

public interface IConversationMessageUpsertRepository
{
    Task<ConversationEntity> AppendUserMessage(
        long userId,
        ChatConfigurationEntity chatConfiguration,
        AppendConversation appendConversation);
    
    void ReplyToLatestMessage(
        ConversationEntity conversation,
        MessageEntityId assistantMessageEntityId,
        ContentEntityId assistantMessageContentId,
        ContentType contentType,
        int sortOrder,
        PromptEntity promptEntity);
}