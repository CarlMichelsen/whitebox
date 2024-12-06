using Database.Entity;
using Domain.Conversation.Action;

namespace Interface.Repository;

public interface IConversationMessageUpsertRepository
{
    Task<ConversationEntity> AppendUserMessage(
        long userId,
        AppendConversation appendConversation);
}