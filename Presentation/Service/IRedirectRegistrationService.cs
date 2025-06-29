using Database.Entity;

namespace Presentation.Service;

public interface IRedirectRegistrationService
{
    Task<RedirectEntity?> RegisterRedirect(string base64Url);
}