using Database;
using Database.Entity;
using Database.Entity.Id;
using Domain.User;
using Interface.Repository;
using LLMIntegration.Util;
using Microsoft.EntityFrameworkCore;

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
            var confId = new UserChatConfigurationId(Guid.CreateVersion7());
            
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
            };
            
            chatConfiguration.User = user;
            await applicationContext.User.AddAsync(user);
        }

        return user.ChatConfiguration!;
    }
}