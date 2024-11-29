using Interface.Dto.Conversation;
using Interface.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class ConversationEndpoints
{
    public static void RegisterConversationEndpoints(
        this IEndpointRouteBuilder apiGroup)
    {
        apiGroup.MapPost(
            "/",
            async ([FromServices] IConversationHandler handler, [FromBody] AppendConversationDto appendConversation) =>
                await handler.Append(appendConversation));
    }
}