using Application.Mapper;
using Database;
using Database.Entity.Id;
using Interface.Accessor;
using Interface.Dto;
using Interface.Dto.Conversation;
using Interface.Repository;
using Interface.Service;
using Microsoft.EntityFrameworkCore;

namespace Application.Service;

public class ConversationService(
    IUserContextAccessor contextAccessor,
    ApplicationContext applicationContext,
    IFullConversationReaderRepository fullConversationReaderRepository) : IConversationService
{
    public async Task<ServiceResponse<ConversationDto>> GetConversation(ConversationEntityId conversationId)
    {
        var user = contextAccessor.GetUserContext().User;
        var conv = await fullConversationReaderRepository.GetConversation(user.Id, conversationId);
        if (conv is null)
        {
            return new ServiceResponse<ConversationDto>("Conversation not found");
        }

        var conversationDto = ConversationMapper.Map(conv, user);
        return new ServiceResponse<ConversationDto>(conversationDto);
    }

    public async Task<ServiceResponse<List<ConversationOptionDto>>> GetConversationList()
    {
        var user = contextAccessor.GetUserContext().User;
        var conversations = await applicationContext.Conversation
            .Where(c => c!.CreatorId == user.Id)
            .ToListAsync();
        
        var options = conversations
            .Select(c => ConversationOptionMapper.Map(c!))
            .ToList();
        
        return new ServiceResponse<List<ConversationOptionDto>>(options);
    }
}