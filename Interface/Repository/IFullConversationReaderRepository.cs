using Database.Entity;
using Database.Entity.Id;

namespace Interface.Repository;

public interface IFullConversationReaderRepository
{
    Task<ConversationEntity?> GetConversation(long userId, ConversationEntityId conversationId);
}