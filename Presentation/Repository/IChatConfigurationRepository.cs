using Database.Entity;
using Domain.User;

namespace Presentation.Repository;

public interface IChatConfigurationRepository
{
    Task<ChatConfigurationEntity> GetOrCreateChatConfigurationAsync(AuthenticatedUser authenticatedUser);
}