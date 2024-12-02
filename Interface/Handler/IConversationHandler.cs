using Interface.Dto.Conversation.Request;

namespace Interface.Handler;

public interface IConversationHandler
{
    Task Append(AppendConversationDto appendConversation);
}