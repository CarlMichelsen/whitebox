﻿using Database.Entity;
using LLMIntegration;
using Presentation.Dto.Configuration;

namespace Application.Mapper;

public static class ChatConfigurationMapper
{
    public static ChatConfigurationDto Map(ChatConfigurationEntity entity)
    {
        if (!LlmModels.TryGetModel(entity.SelectedModelIdentifier, out var model))
        {
            throw new Exception("Selected model is not valid");
        }
        
        return new ChatConfigurationDto(
            Id: entity.Id.Value.ToString(),
            DefaultSystemMessage: entity.DefaultSystemMessage,
            SelectedModel: AvailableModelsMapper.Map(model!),
            MaxTokens: entity.MaxTokens,
            AvailableProviders: AvailableModelsMapper.GetProviders());
    }
}