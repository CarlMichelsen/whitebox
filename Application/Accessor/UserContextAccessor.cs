using Domain.User;
using Interface.Accessor;
using Microsoft.AspNetCore.Http;

namespace Application.Accessor;

public class UserContextAccessor(
    IHttpContextAccessor httpContextAccessor) : IUserContextAccessor
{
    public UserContext GetUserContext(HttpContext? httpContext = default)
    {
        var context = httpContextAccessor.HttpContext ?? httpContext;
        if (context is null)
        {
            throw new ArgumentNullException(nameof(httpContext));
        }
        
        return HttpUserContextAccessor.GetUserContext(context);
    }
}