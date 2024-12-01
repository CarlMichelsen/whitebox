using Database.Entity;
using Domain.User;

namespace Interface.Repository;

public interface IChatConfigurationRepository
{
    Task<ChatConfigurationEntity> GetOrCreateChatConfigurationAsync(AuthenticatedUser authenticatedUser);
}