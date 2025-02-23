using Interface.Dto.Conversation.Request;
using Microsoft.AspNetCore.Http;

namespace Interface.Handler;

public interface IConversationHandler
{
    Task<IResult> GetConversation(Guid conversationId);
    
    Task<IResult> SetConversationSystemMessage(Guid conversationId, SetConversationSystemMessage systemMessage);
    
    Task<IResult> GetConversationList();
}