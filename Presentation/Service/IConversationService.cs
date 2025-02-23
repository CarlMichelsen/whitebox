using Database.Entity.Id;
using Interface.Dto;
using Interface.Dto.Conversation;
using Interface.Dto.Conversation.Request;
using Interface.Dto.Conversation.Response;

namespace Interface.Service;

public interface IConversationService
{
    Task<ServiceResponse<ConversationDto>> GetConversation(ConversationEntityId conversationId);
    
    Task<ServiceResponse<SetSystemMessageResponseDto>> SetConversationSystemMessage(ConversationEntityId conversationId, SetConversationSystemMessage setConversationSystemMessage);
    
    Task<ServiceResponse<List<ConversationOptionDto>>> GetConversationList();
}