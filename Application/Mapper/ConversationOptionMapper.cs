using Database.Entity;
using Interface.Dto.Conversation;

namespace Application.Mapper;

public static class ConversationOptionMapper
{
    public static ConversationOptionDto Map(ConversationEntity conversationEntity)
    {
        return new ConversationOptionDto(
            Id: conversationEntity.Id.Value,
            Title: conversationEntity.Summary ?? "New Conversation",
            LastAltered: TimeMapper.GetUnixTimeSeconds(conversationEntity.LastAlteredUtc));
    }
}