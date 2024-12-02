using Domain.User;
using Interface.Accessor;
using Microsoft.AspNetCore.Http;

namespace Test.Fake;

public class FakeUserContextAccessor : IUserContextAccessor
{
    public UserContext GetUserContext(HttpContext? httpContext = default)
    {
        return new UserContext(
            LoginId: 1,
            RefreshId: 1,
            AccessId: 1,
            User: new AuthenticatedUser(
                Id: 1,
                AuthenticationMethod: "Fake",
                AuthenticationId: "external authentication id",
                Username: "FakeUser",
                Email: "fake@email.com",
                AvatarUrl: "fake avatar url"));
    }
}