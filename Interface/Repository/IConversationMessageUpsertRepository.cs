using Database.Entity;
using Domain.Conversation.Action;

namespace Interface.Repository;

public interface IConversationMessageUpsertRepository
{
    Task<(ConversationEntity Conversation, MessageEntity Message)> AppendUserMessage(
        long userId,
        AppendConversation appendConversation);
}