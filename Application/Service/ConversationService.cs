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
    ICacheService cacheService,
    IFullConversationReaderRepository fullConversationReaderRepository) : IConversationService
{
    public async Task<ServiceResponse<ConversationDto>> GetConversation(ConversationEntityId conversationId)
    {
        var user = contextAccessor.GetUserContext().User;
        var cacheKey = ConversationMapper.CacheKeyFactory(user, conversationId);
        
        var conversationDto = await cacheService.CacheFactory(cacheKey, TimeSpan.FromHours(1), async () =>
        {
            var conv = await fullConversationReaderRepository.GetConversation(user.Id, conversationId);
            return conv is null
                ? null
                : ConversationMapper.Map(conv, user);
        });

        return conversationDto is null
            ? new ServiceResponse<ConversationDto>("Conversation not found")
            : new ServiceResponse<ConversationDto>(conversationDto);
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