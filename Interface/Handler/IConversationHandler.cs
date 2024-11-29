using Interface.Dto.Conversation;
using Microsoft.AspNetCore.Http;

namespace Interface.Handler;

public interface IConversationHandler
{
    Task<IResult> Append(AppendConversationDto appendConversation);
}