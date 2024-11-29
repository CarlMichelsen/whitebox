using Interface.Dto.Conversation;
using Interface.Handler;
using Microsoft.AspNetCore.Http;

namespace Application.Handler;

public class ConversationHandler(
    IHttpContextAccessor httpContextAccessor) : IConversationHandler
{
    public Task<IResult> Append(AppendConversationDto appendConversation)
    {
        throw new NotImplementedException();
    }
}