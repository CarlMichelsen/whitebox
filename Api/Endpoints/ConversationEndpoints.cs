using Interface.Dto;
using Interface.Dto.Conversation;
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
            async ([FromServices] IConversationAppendHandler handler, [FromBody] AppendConversationDto appendConversation) =>
                await handler.Append(appendConversation));
        
        conversationGroup.MapGet(
            "/",
            async ([FromServices] IConversationHandler handler) =>
            await handler.GetConversationList())
            .Produces<ServiceResponse<List<ConversationOptionDto>>>();
        
        conversationGroup.MapGet(
            "/{id:guid}",
            async ([FromServices] IConversationHandler handler, [FromRoute] Guid id) =>
            await handler.GetConversation(id))
            .Produces<ServiceResponse<ConversationDto>>();
    }
}