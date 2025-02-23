using Database;
using Database.Entity.Id;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class ConversationManagementRepository(
    ApplicationContext applicationContext) : IConversationManagementRepository
{
    public async Task<string> SetConversationSystemMessage(
        ConversationEntityId conversationId,
        long userId,
        string newSystemMessage)
    {
        var nullIfEmpty = string.IsNullOrWhiteSpace(newSystemMessage) ? null : newSystemMessage;
        
        var rows = await applicationContext.Conversation
            .Where(c => c.Id == conversationId && c.CreatorId == userId)
            .ExecuteUpdateAsync(c => c.SetProperty(conv => conv.SystemMessage, nullIfEmpty));

        if (rows != 1)
        {
            throw new Exception("Failed to set conversation system message");
        }
        
        return newSystemMessage;
    }
}