using Database.Entity.Id;

namespace Interface.Repository;

public interface IConversationManagementRepository
{
    Task<string> SetConversationSystemMessage(ConversationEntityId conversationId, long userId, string newSystemMessage);
}