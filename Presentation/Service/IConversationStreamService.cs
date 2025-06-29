using Domain.Conversation.Action;
using Presentation.Dto.Conversation.Response.Stream;

namespace Presentation.Service;

public interface IConversationStreamService
{
    Task GetConversationResponse(
        AppendConversation appendConversation,
        Func<BaseStreamResponseDto, Task> handler);
}