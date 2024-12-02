using Interface.Dto.Conversation.Request;
using Interface.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class ConversationEndpoints
{
    public static void RegisterConversationEndpoints(
        this IEndpointRouteBuilder apiGroup)
    {
        var conversationGroup = apiGroup
            .MapGroup("conversation")
            .WithTags("Conversation");
        
        conversationGroup.MapPost(
            "/",
            async ([FromServices] IConversationHandler handler, [FromBody] AppendConversationDto appendConversation) =>
                await handler.Append(appendConversation));
    }
}