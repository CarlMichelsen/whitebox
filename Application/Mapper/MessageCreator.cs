using Database.Entity;
using Database.Entity.Id;

namespace Application.Mapper;

public static class MessageCreator
{
    public static MessageEntity CreateMessageFromText(
        ConversationEntity conversation,
        PromptEntity? prompt,
        string text,
        MessageEntity? previousMessage = default)
    {
        var msg = new MessageEntity
        {
            Id = new MessageEntityId(Guid.CreateVersion7()),
            ConversationId = conversation.Id,
            Conversation = conversation,
            PromptId = prompt?.Id,
            Prompt = prompt,
            NextMessages = [],
            PreviousMessageId = previousMessage?.Id,
            PreviousMessage = previousMessage,
            Content = [],
            CreatedUtc = DateTime.UtcNow,
        };

        var content = new ContentEntity
        {
            Id = new ContentEntityId(Guid.CreateVersion7()),
            MessageId = msg.Id,
            Message = msg,
            Type = ContentType.Text,
            Value = text,
            SortOrder = 10,
        };
        
        msg.Content.Add(content);
        return msg;
    }
}