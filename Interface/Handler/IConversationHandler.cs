using Interface.Dto.Conversation;
using Microsoft.AspNetCore.Http;

namespace Interface.Handler;

public interface IConversationHandler
{
    Task Append(AppendConversationDto appendConversation);
}