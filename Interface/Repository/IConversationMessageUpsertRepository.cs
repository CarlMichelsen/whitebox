using Database.Entity;
using Database.Entity.Id;
using Domain.Conversation.Action;

namespace Interface.Repository;

public interface IConversationMessageUpsertRepository
{
    Task<ConversationEntity> AppendUserMessage(
        long userId,
        ChatConfigurationEntity chatConfiguration,
        AppendConversation appendConversation);
    
    void ReplyToLatestMessage(
        ConversationEntity conversation,
        MessageEntityId promptMessageEntityId,
        PromptEntity promptEntity);
}