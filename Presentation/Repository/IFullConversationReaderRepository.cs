using Database.Entity;
using Database.Entity.Id;

namespace Presentation.Repository;

public interface IFullConversationReaderRepository
{
    Task<ConversationEntity?> GetConversation(long userId, ConversationEntityId conversationId);
}