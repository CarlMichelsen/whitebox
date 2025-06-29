using Database.Entity.Id;

namespace Presentation.Repository;

public interface IConversationManagementRepository
{
    Task<string> SetConversationSystemMessage(ConversationEntityId conversationId, long userId, string newSystemMessage);
}