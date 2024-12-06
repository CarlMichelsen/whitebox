using Domain.Conversation.Action;
using Interface.Dto.Conversation.Response.Stream;

namespace Interface.Service;

public interface IConversationResponseStreamService
{
    IAsyncEnumerable<BaseStreamResponseDto> GetConversationResponse(AppendConversation appendConversation);
}