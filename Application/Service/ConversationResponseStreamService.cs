using Application.Mapper;
using Database;
using Domain.Conversation.Action;
using Interface.Accessor;
using Interface.Dto.Conversation.Response.Stream;
using Interface.Repository;
using Interface.Service;

namespace Application.Service;

public class ConversationResponseStreamService(
    ApplicationContext applicationContext,
    IUserContextAccessor contextAccessor,
    IConversationMessageUpsertRepository conversationMessageUpsertRepository) : IConversationResponseStreamService
{
    public async IAsyncEnumerable<BaseStreamResponseDto> GetConversationResponse(
        AppendConversation appendConversation)
    {
        var user = contextAccessor.GetUserContext().User;
        var conversation = await conversationMessageUpsertRepository
            .AppendUserMessage(user.Id, appendConversation);

        yield return new ConversationEventDto
        {
            ConversationId = conversation.Id.Value,
        };
        
        yield return new UserMessageEventDto
        {
            Message = ConversationMapper.Map(conversation.LastAppendedMessage!),
        };
        
        conversation.LastAlteredUtc = DateTime.UtcNow;
        await applicationContext.SaveChangesAsync();
        yield break;
    }
}