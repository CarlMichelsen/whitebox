using Microsoft.AspNetCore.Http;

namespace Interface.Handler;

public interface IConversationHandler
{
    Task<IResult> GetConversation(Guid conversationId);
    
    Task<IResult> GetConversationList();
}