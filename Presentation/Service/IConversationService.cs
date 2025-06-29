using Database.Entity.Id;
using Presentation.Dto;
using Presentation.Dto.Conversation;
using Presentation.Dto.Conversation.Request;
using Presentation.Dto.Conversation.Response;

namespace Presentation.Service;

public interface IConversationService
{
    Task<ServiceResponse<ConversationDto>> GetConversation(ConversationEntityId conversationId);
    
    Task<ServiceResponse<SetSystemMessageResponseDto>> SetConversationSystemMessage(ConversationEntityId conversationId, SetConversationSystemMessage setConversationSystemMessage);
    
    Task<ServiceResponse<List<ConversationOptionDto>>> GetConversationList();
}