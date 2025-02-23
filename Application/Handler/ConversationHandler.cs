using Database.Entity.Id;
using Interface.Dto;
using Interface.Dto.Conversation;
using Interface.Dto.Conversation.Request;
using Interface.Dto.Conversation.Response;
using Interface.Handler;
using Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handler;

public class ConversationHandler(
    ILogger<ConversationHandler> logger,
    IConversationService conversationService) : IConversationHandler
{
    public async Task<IResult> GetConversation(Guid conversationId)
    {
        try
        {
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

    public async Task<IResult> SetConversationSystemMessage(Guid conversationId, SetConversationSystemMessage systemMessage)
    {
        try
        {
            var res = await conversationService.SetConversationSystemMessage(
                new ConversationEntityId(conversationId),
                systemMessage);
            return Results.Ok(res);
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "ConversationHandler failed set conversation system message");  
            return Results.Ok(new ServiceResponse<SetSystemMessageResponseDto>("Exception"));
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