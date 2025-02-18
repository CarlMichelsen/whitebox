using Interface.Dto.Conversation.Request;

namespace Interface.Handler;

public interface IConversationStreamHandler
{
    Task Append(AppendConversationDto appendConversation);
}