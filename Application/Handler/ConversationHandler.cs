using Interface.Handler;
using Microsoft.AspNetCore.Http;

namespace Application.Handler;

public class ConversationHandler : IConversationHandler
{
    public async Task<IResult> GetConversation(Guid conversationId)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> GetConversationList()
    {
        throw new NotImplementedException();
    }
}