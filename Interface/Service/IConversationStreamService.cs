using Domain.Conversation.Action;
using Interface.Dto.Conversation.Response.Stream;

namespace Interface.Service;

public interface IConversationStreamService
{
    Task GetConversationResponse(
        AppendConversation appendConversation,
        Func<BaseStreamResponseDto, Task> handler);
}