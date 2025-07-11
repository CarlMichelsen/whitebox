﻿using Database;
using Database.Entity;
using Database.Entity.Id;
using Domain.User;
using LLMIntegration;
using Microsoft.EntityFrameworkCore;
using Presentation.Repository;

namespace Application.Repository;

public class ChatConfigurationRepository(
    ApplicationContext applicationContext) : IChatConfigurationRepository
{
    public async Task<ChatConfigurationEntity> GetOrCreateChatConfigurationAsync(AuthenticatedUser authenticatedUser)
    {
        var user = await applicationContext.User
            .Include(u => u.ChatConfiguration)
            .FirstOrDefaultAsync(u => u.Id == authenticatedUser.Id);

        if (user is null)
        {
            var confId = new UserChatConfigurationEntityId(Guid.CreateVersion7());
            
            var chatConfiguration = new ChatConfigurationEntity
            {
                Id = confId,
                UserId = authenticatedUser.Id,
                DefaultSystemMessage = null,
                SelectedModelIdentifier = LlmModels.OpenAi.Gpt4O.ModelIdentifier,
                MaxTokens = 65536,
            };

            user = new UserEntity
            {
                Id = authenticatedUser.Id,
                AuthenticationMethod = authenticatedUser.AuthenticationMethod,
                AuthenticationId = authenticatedUser.AuthenticationId,
                ChatConfigurationId = confId,
                ChatConfiguration = chatConfiguration,
                Conversations = [],
                Prompts = [],
                FirstLoginUtc = DateTime.UtcNow,
            };
            
            chatConfiguration.User = user;
            await applicationContext.User.AddAsync(user);
        }

        return user.ChatConfiguration!;
    }
}