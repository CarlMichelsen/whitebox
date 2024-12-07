using Database.Entity.Id;
using Interface.Dto;
using Interface.Dto.Conversation;
using Interface.Handler;
using Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handler;

public class ConversationHandler(
    ILogger<ConversationHandler> logger,
    ICacheService cacheService,
    IConversationService conversationService) : IConversationHandler
{
    public async Task<IResult> GetConversation(Guid conversationId)
    {
        try
        {
            var cachedConversation = await cacheService.Get<ConversationDto>(conversationId.ToString());
            if (cachedConversation is not null)
            {
                return Results.Ok(new ServiceResponse<ConversationDto>(cachedConversation));
            }
            
            var conversationEntityId = new ConversationEntityId(conversationId);
            var conversationResponse = await conversationService.GetConversation(conversationEntityId);
            return Results.Ok(conversationResponse);
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "ConversationHandler failed to get conversation");  
            return Results.Ok(new ServiceResponse<ConversationDto>("Exception"));
        }
    }

    public async Task<IResult> GetConversationList()
    {
        try
        {
            var conversationOptionResponse = await conversationService.GetConversationList();
            return Results.Ok(conversationOptionResponse);
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "ConversationHandler failed to get conversation list");  
            return Results.Ok(new ServiceResponse<List<ConversationOptionDto>>("Exception"));
        }
    }
}