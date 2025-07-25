﻿using Application.Mapper;
using Database;
using Database.Entity;
using Database.Entity.Id;
using Domain.Conversation.Action;
using LLMIntegration.Generic.Dto.Response.Stream;
using Presentation.Accessor;
using Presentation.Dto.Conversation;
using Presentation.Dto.Conversation.Response.Stream;
using Presentation.Repository;
using Presentation.Service;

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
        var cacheKey = ConversationMapper.CacheKeyFactory(user, conversation.Id);

        await cacheService.Set(
            cacheKey,
            ConversationMapper.Map(conversation, user), 
            TimeSpan.FromMinutes(2));

        if (string.IsNullOrWhiteSpace(conversation.Summary))
        {
            await handler(new SetSummaryEventDto
            {
                ConversationId = conversation.Id.Value,
                Summary = "New Conversation",
            });
        }

        await handler(new ConversationEventDto
        {
            ConversationId = conversation.Id.Value,
        });

        await handler(new UserMessageEventDto
        {
            ConversationId = conversation.Id.Value,
            Message = ConversationMapper.Map(conversation.LastAppendedMessage!, conversation.Id.Value),
        });
        
        var assistantMessageId = new MessageEntityId(Guid.CreateVersion7());
        var assistantMessageContentId = new ContentEntityId(Guid.CreateVersion7());
        const ContentType contentType = ContentType.Text;
        const int sortOrder = 10;
        var prompt = PromptConversationMapper
            .CreatePromptFromLatestUserMessage(conversation, chatConfig.SelectedModelIdentifier, chatConfig.MaxTokens);
        
        await handler(new AssistantMessageEventDto
        {
            ConversationId = conversation.Id.Value,
            MessageId = assistantMessageId.Value,
            ReplyToMessageId = conversation.LastAppendedMessage!.Id.Value,
            Model = AvailableModelsMapper.Map(prompt.Model),
        });
        
        var newPromptEntity = await promptService
            .StreamPrompt(prompt, async streamEvent =>
        {
            switch (streamEvent)
            {
                case LlmStreamContentDelta delta:
                    await handler(new AssistantMessageDeltaEventDto
                    {
                        ConversationId = conversation.Id.Value,
                        MessageId = assistantMessageId.Value,
                        ContentId = assistantMessageContentId.Value,
                        ContentType = ConversationMapper.Map(contentType),
                        SortOrder = sortOrder,
                        ContentDelta = delta.Delta.Content,
                    });
                    break;
                case LlmStreamError error:
                    await handler(new ErrorEventDto
                    {
                        ConversationId = conversation.Id.Value,
                        Error = error.Error,
                    });
                    break;
            }
        });

        if (newPromptEntity.Usage is not null)
        {
            await handler(new AssistantUsageEventDto
            {
                ConversationId = conversation.Id.Value,
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
                ConversationId = conversation.Id.Value,
                Error = "Prompt did not conclude properly",
            });
            return;
        }
        
        conversationMessageUpsertRepository.ReplyToLatestMessage(
            conversation,
            assistantMessageId,
            assistantMessageContentId,
            contentType,
            sortOrder,
            newPromptEntity);

        if (string.IsNullOrWhiteSpace(conversation.Summary))
        {
            var summaryPrompt = SummaryPromptMapper.SummaryPrompt(conversation);
            var res = await promptService.Prompt(summaryPrompt);
            conversation.Summary = res.Usage!.Completion;
            
            await handler(new SetSummaryEventDto
            {
                ConversationId = conversation.Id.Value,
                Summary = conversation.Summary,
            });
        }
        
        conversation.LastAlteredUtc = DateTime.UtcNow;
        await applicationContext.SaveChangesAsync();
        await cacheService.Set(
            cacheKey,
            ConversationMapper.Map(conversation, user),
            TimeSpan.FromHours(2));
    }
}