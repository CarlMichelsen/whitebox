using Domain.User;
using Microsoft.AspNetCore.Http;

namespace Interface.Accessor;

public interface IUserContextAccessor
{
    UserContext GetUserContext(HttpContext? httpContext = default);
}