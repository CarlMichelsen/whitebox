using Interface.Dto.Conversation.Request;

namespace Interface.Handler;

public interface IConversationAppendHandler
{
    Task Append(AppendConversationDto appendConversation);
}