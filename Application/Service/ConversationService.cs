using Application.Mapper;
using Database;
using Database.Entity.Id;
using Interface.Accessor;
using Interface.Dto;
using Interface.Dto.Conversation;
using Interface.Dto.Conversation.Request;
using Interface.Dto.Conversation.Response;
using Interface.Repository;
using Interface.Service;
using Microsoft.EntityFrameworkCore;

namespace Application.Service;

public class ConversationService(
    IUserContextAccessor contextAccessor,
    ApplicationContext applicationContext,
    ICacheService cacheService,
    IFullConversationReaderRepository fullConversationReaderRepository,
    IConversationManagementRepository conversationManagementRepository) : IConversationService
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

    public async Task<ServiceResponse<SetSystemMessageResponseDto>> SetConversationSystemMessage(
        ConversationEntityId conversationId,
        SetConversationSystemMessage setConversationSystemMessage)
    {
        var user = contextAccessor.GetUserContext().User;
        var cacheKey = ConversationMapper.CacheKeyFactory(user, conversationId);

        var currentSystemMessage = await conversationManagementRepository.SetConversationSystemMessage(
            conversationId,
            user.Id,
            setConversationSystemMessage.SystemMessage);

        var dto = await cacheService.Get<ConversationDto>(cacheKey);
        if (dto is not null)
        {
            var systemMessageEdited = dto with { SystemMessage = currentSystemMessage };
            await cacheService.Set(cacheKey, systemMessageEdited, TimeSpan.FromHours(1));
        }
            
        return new ServiceResponse<SetSystemMessageResponseDto>(new SetSystemMessageResponseDto(
            ConversationId: conversationId.Value,
            CurrentSystemMessage: currentSystemMessage));
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