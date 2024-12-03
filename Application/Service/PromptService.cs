﻿using System.Text;
using System.Text.Json;
using Database;
using Database.Entity;
using Database.Entity.Id;
using Interface.Accessor;
using Interface.Service;
using LLMIntegration.Generic;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response;
using LLMIntegration.Generic.Dto.Response.Stream;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Service;

public class PromptService(
    ILogger<PromptService> logger,
    IUserContextAccessor userContextAccessor,
    GenericLlmClient genericLlmClient,
    ApplicationContext applicationContext) : IPromptService
{
    public async Task<PromptEntity> Prompt(LlmPrompt prompt)
    {
        var user = await this.GetUser();
        var json = JsonSerializer.Serialize(prompt);
        var promptEntity = new PromptEntity
        {
            Id = new PromptEntityId(Guid.CreateVersion7()),
            UsageId = null,
            Usage = null,
            UserId = user.Id,
            User = user,
            PromptJson = json,
            PromptUtc = DateTime.UtcNow,
            Stream = false,
        };
        applicationContext.Prompt.Add(promptEntity);

        LlmResponse? response = default;

        try
        {
            response = await genericLlmClient.Prompt(prompt);
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "Failed to prompt");
        }
        
        if (response is null)
        {
            return promptEntity;
        }
        
        var usage = new UsageEntity
        {
            Id = new UsageEntityId(Guid.CreateVersion7()),
            Provider = Enum.GetName(prompt.Model.Provider)!,
            ModelIdentifier = prompt.Model.ModelIdentifier,
            PromptId = promptEntity.Id,
            Prompt = promptEntity,
            InputTokens = response.Usage.InputTokens,
            OutputTokens = response.Usage.OutputTokens,
            CompleteUtc = DateTime.UtcNow,
            Completion = string.Join('\n', response.Parts),
        };
        
        promptEntity.UsageId = usage.Id;
        promptEntity.Usage = usage;
        return promptEntity;
    }

    public async Task<PromptEntity> StreamPrompt(LlmPrompt prompt, Func<LlmStreamEvent, Task> handler)
    {
        LlmStreamConclusion? streamConclusion = default;
        var user = await this.GetUser();
        var json = JsonSerializer.Serialize(prompt);
        var promptEntity = new PromptEntity
        {
            Id = new PromptEntityId(Guid.CreateVersion7()),
            UsageId = null,
            Usage = null,
            UserId = user.Id,
            User = user,
            PromptJson = json,
            PromptUtc = DateTime.UtcNow,
            Stream = true,
        };
        applicationContext.Prompt.Add(promptEntity);
        var sb = new StringBuilder();
        
        try
        {
            await foreach (var streamEvent in genericLlmClient.StreamPrompt(prompt))
            {
                switch (streamEvent)
                {
                    case LlmStreamContentDelta contentDelta:
                        sb.Append(contentDelta.Delta);
                        break;
                    case LlmStreamConclusion conclusion:
                        streamConclusion = conclusion;
                        break;
                }

                await handler(streamEvent);
            }
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "Failed to stream prompt");
        }

        if (streamConclusion is null)
        {
            return promptEntity;
        }

        var usage = new UsageEntity
        {
            Id = new UsageEntityId(Guid.CreateVersion7()),
            Provider = Enum.GetName(prompt.Model.Provider)!,
            ModelIdentifier = prompt.Model.ModelIdentifier,
            PromptId = promptEntity.Id,
            Prompt = promptEntity,
            InputTokens = streamConclusion.Usage.InputTokens,
            OutputTokens = streamConclusion.Usage.OutputTokens,
            CompleteUtc = DateTime.UtcNow,
            Completion = sb.ToString(),
        };
        
        promptEntity.UsageId = usage.Id;
        promptEntity.Usage = usage;
        return promptEntity;
    }

    private Task<UserEntity> GetUser()
    {
        var user = userContextAccessor.GetUserContext().User;
        return applicationContext.User.FirstAsync(u => u.Id == user.Id);
    }
}