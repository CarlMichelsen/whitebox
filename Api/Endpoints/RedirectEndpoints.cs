using Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class RedirectEndpoints
{
    public static void RegisterRedirectEndpoints(
        this IEndpointRouteBuilder apiGroup)
    {
        var conversationGroup = apiGroup
            .MapGroup("redirect")
            .WithTags("Redirect");
        
        conversationGroup
            .MapGet("chatLink/{base64Url}", async (string base64Url, [FromServices] IRedirectRegistrationService redirectRegistrationService) =>
            {
                var redirectEntity = await redirectRegistrationService.RegisterRedirect(base64Url);
                return redirectEntity is not null
                    ? Results.Redirect(redirectEntity.Url)
                    : Results.BadRequest();
            });
    }
}