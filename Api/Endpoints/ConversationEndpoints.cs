using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using Presentation.Dto.Conversation;
using Presentation.Dto.Conversation.Request;
using Presentation.Dto.Conversation.Response;
using Presentation.Handler;

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
            async ([FromServices] IConversationStreamHandler handler, [FromBody] AppendConversationDto appendConversation) =>
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
        
        conversationGroup.MapDelete(
                "/{id:guid}",
                async ([FromServices] IConversationHandler handler, [FromRoute] Guid id) =>
                await handler.DeleteConversation(id))
            .Produces<ServiceResponse>();
        
        conversationGroup.MapPatch(
                "/{id:guid}",
                async ([FromServices] IConversationHandler handler, [FromRoute] Guid id, [FromBody] SetConversationSystemMessage systemMessage) =>
                await handler.SetConversationSystemMessage(id, systemMessage))
            .Produces<ServiceResponse<SetSystemMessageResponseDto>>();
    }
}