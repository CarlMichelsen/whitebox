using Database.Entity.Id;
using Interface.Dto;
using Interface.Dto.Conversation;

namespace Interface.Service;

public interface IConversationService
{
    Task<ServiceResponse<ConversationDto>> GetConversation(ConversationEntityId conversationId);
    
    Task<ServiceResponse<List<ConversationOptionDto>>> GetConversationList();
}