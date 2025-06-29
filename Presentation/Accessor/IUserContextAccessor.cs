using Domain.User;
using Microsoft.AspNetCore.Http;

namespace Presentation.Accessor;

public interface IUserContextAccessor
{
    UserContext GetUserContext(HttpContext? httpContext = default);
}