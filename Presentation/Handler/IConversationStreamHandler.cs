using Presentation.Dto.Conversation.Request;

namespace Presentation.Handler;

public interface IConversationStreamHandler
{
    Task Append(AppendConversationDto appendConversation);
}