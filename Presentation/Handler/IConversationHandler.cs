using Microsoft.AspNetCore.Http;
using Presentation.Dto.Conversation.Request;

namespace Presentation.Handler;

public interface IConversationHandler
{
    Task<IResult> GetConversation(Guid conversationId);
    
    Task<IResult> DeleteConversation(Guid conversationId);
    
    Task<IResult> SetConversationSystemMessage(Guid conversationId, SetConversationSystemMessage systemMessage);
    
    Task<IResult> GetConversationList();
}