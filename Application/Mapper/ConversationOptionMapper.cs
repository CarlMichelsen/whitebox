using Database.Entity;
using Presentation.Dto.Conversation;

namespace Application.Mapper;

public static class ConversationOptionMapper
{
    public static ConversationOptionDto Map(ConversationEntity conversationEntity)
    {
        return new ConversationOptionDto(
            Id: conversationEntity.Id.Value,
            Summary: conversationEntity.Summary ?? "New Conversation",
            LastAltered: TimeMapper.GetUnixTimeSeconds(conversationEntity.LastAlteredUtc));
    }
}