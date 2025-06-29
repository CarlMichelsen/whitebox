using Database;
using Database.Entity;
using Database.Entity.Id;
using Domain.Exception;
using Domain.User;
using Presentation.Accessor;
using Presentation.Service;

namespace Application.Service;

public class RedirectRegistrationService(
    ApplicationContext applicationContext,
    IUserContextAccessor userContextAccessor) : IRedirectRegistrationService
{
    private const string PlaceholderSourceId = "0197bb42-a89e-7966-8dc4-280dd9c3d484";
    
    public async Task<RedirectEntity?> RegisterRedirect(string base64Url)
    {
        AuthenticatedUser? authenticatedUser = default;
        try
        {
            authenticatedUser = userContextAccessor.GetUserContext().User;
        }
        catch (UserException)
        {
            /* Ignore */
        }

        UserEntity? userEntity = default;
        if (authenticatedUser is not null)
        {
            userEntity = await applicationContext.User.FindAsync(authenticatedUser.Id);
        }
        
        var bytes = Convert.FromBase64String(base64Url);
        var url = new string(System.Text.Encoding.UTF8.GetChars(bytes));
        var unescaped = Uri.UnescapeDataString(url);
        if (!Uri.TryCreate(unescaped, UriKind.Absolute, out var uri))
        {
            return default;
        }

        var redirect = new RedirectEntity
        {
            Id = new RedirectEntityId(Guid.CreateVersion7()),
            Url = uri.AbsoluteUri,
            UserId = userEntity?.Id,
            User = userEntity,
            SourceId = new SourceId(Guid.Parse(PlaceholderSourceId)),
            RedirectedAtUtc = DateTime.UtcNow,
        };

        applicationContext.Redirect.Add(redirect);
        await applicationContext.SaveChangesAsync();
        
        return redirect;
    }
}