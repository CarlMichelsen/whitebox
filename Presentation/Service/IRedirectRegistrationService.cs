using Database.Entity;

namespace Interface.Service;

public interface IRedirectRegistrationService
{
    Task<RedirectEntity?> RegisterRedirect(string base64Url);
}