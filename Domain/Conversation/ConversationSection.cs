using Database.Entity;
using Database.Entity.Id;

namespace Domain.Conversation;

public class ConversationSection
{
    public required MessageEntityId? SelectedMessageId { get; set; }
    
    public required Dictionary<MessageEntityId, MessageEntity> Messages { get; init; }
}