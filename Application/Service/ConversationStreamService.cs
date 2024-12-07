using System.Text.Json;
using Application.Mapper;
using Database;
using Database.Entity;
using Database.Entity.Id;
using Domain.Conversation.Action;
using Interface.Accessor;
using Interface.Dto.Conversation;
using Interface.Dto.Conversation.Response.Stream;
using Interface.Repository;
using Interface.Service;
using LLMIntegration.Generic.Dto.Response.Stream;

namespace Application.Service;

public class ConversationStreamService(
    ApplicationContext applicationContext,
    IUserContextAccessor contextAccessor,
    IChatConfigurationRepository chatConfigurationRepository,
    IConversationMessageUpsertRepository conversationMessageUpsertRepository,
    ICacheService cacheService,
    IPromptService promptService) : IConversationStreamService
{
    public async Task GetConversationResponse(
        AppendConversation appendConversation,
        Func<BaseStreamResponseDto, Task> handler)
    {
        var user = contextAccessor.GetUserContext().User;
        var chatConfig = await chatConfigurationRepository
            .GetOrCreateChatConfigurationAsync(user);
        
        var conversation = await conversationMessageUpsertRepository
            .AppendUserMessage(user.Id, chatConfig, appendConversation);

        await cacheService.Set(
            conversation.Id.Value.ToString(),
            ConversationMapper.Map(conversation, user), 
            TimeSpan.FromMinutes(2));

        await handler(new ConversationEventDto
        {
            ConversationId = conversation.Id.Value,
        });

        await handler(new UserMessageEventDto
        {
            Message = ConversationMapper.Map(conversation.LastAppendedMessage!),
        });
        
        var assistantMessageId = new MessageEntityId(Guid.CreateVersion7());
        await handler(new AssistantMessageEventDto
        {
            MessageId = assistantMessageId.Value,
            ReplyToMessageId = conversation.LastAppendedMessage!.Id.Value,
        });
        
        var prompt = PromptConversationMapper
            .CreatePromptFromLatestUserMessage(conversation, chatConfig);
        var newPromptEntity = await promptService
            .StreamPrompt(prompt, async streamEvent =>
        {
            switch (streamEvent)
            {
                case LlmStreamContentDelta delta:
                    await handler(new AssistantMessageDeltaEventDto
                    {
                        MessageId = assistantMessageId.Value,
                        ContentDelta = delta.Delta.Content,
                    });
                    break;
                case LlmStreamError error:
                    await handler(new ErrorEventDto
                    {
                        Error = error.Error,
                    });
                    break;
            }
        });

        if (newPromptEntity.Usage is not null)
        {
            await handler(new AssistantUsageEventDto
            {
                MessageId = assistantMessageId.Value,
                Usage = new UsageDto(
                    Id: newPromptEntity.Usage.Id.Value,
                    InputTokens: newPromptEntity.Usage.InputTokens,
                    OutputTokens: newPromptEntity.Usage.OutputTokens,
                    InitialModelIdentifier: newPromptEntity.Usage.InitialModelIdentifier,
                    SpecificModelIdentifier: newPromptEntity.Usage.SpecificModelIdentifier),
            });
        }
        else
        {
            await handler(new ErrorEventDto
            {
                Error = "Prompt did not conclude properly",
            });
            return;
        }
        
        conversationMessageUpsertRepository
            .ReplyToLatestMessage(conversation, assistantMessageId, newPromptEntity);

        if (string.IsNullOrWhiteSpace(conversation.Summary))
        {
            var summary = $"Fake Summary ({Random.Shared.Next(100)})";
            conversation.Summary = summary;
            await handler(new SetSummaryEventDto
            {
                ConversationId = conversation.Id.Value,
                Summary = conversation.Summary,
            });
        }
        
        conversation.LastAlteredUtc = DateTime.UtcNow;
        await applicationContext.SaveChangesAsync();
        await cacheService.Set(
            conversation.Id.Value.ToString(),
            ConversationMapper.Map(conversation, user),
            TimeSpan.FromHours(2));
    }
}