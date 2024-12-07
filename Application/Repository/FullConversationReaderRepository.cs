using Database;
using Database.Entity;
using Database.Entity.Id;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class FullConversationReaderRepository(ApplicationContext applicationContext) : IFullConversationReaderRepository
{
    public async Task<ConversationEntity?> GetConversation(long userId, ConversationEntityId conversationId)
    {
        return await applicationContext.Conversation
            .AsSplitQuery()
            .Where(c => c!.CreatorId == userId && c.Id == conversationId)
            .Include(c => c.Messages)
                .ThenInclude(messageEntity => messageEntity.Prompt)
                .ThenInclude(promptEntity => promptEntity!.Usage)
            .Include(c => c.Messages)
                .ThenInclude(messageEntity => messageEntity.Content)
            .Include(c => c.LastAppendedMessage)
            .Include(c => c.Creator)
            .FirstOrDefaultAsync();
    }
}