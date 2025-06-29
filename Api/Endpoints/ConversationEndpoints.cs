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
            "/{conversationId:guid}",
            async ([FromServices] IConversationHandler handler, [FromRoute] Guid conversationId) =>
            await handler.GetConversation(conversationId))
            .Produces<ServiceResponse<ConversationDto>>();
        
        conversationGroup.MapDelete(
                "/{conversationId:guid}",
                async ([FromServices] IConversationHandler handler, [FromRoute] Guid conversationId) =>
                await handler.DeleteConversation(conversationId))
            .Produces<ServiceResponse>();
        
        conversationGroup.MapDelete(
                "/{conversationId:guid}/{messageId:guid}",
                async ([FromServices] IConversationHandler handler, [FromRoute] Guid conversationId, [FromRoute] Guid messageId) =>
                await handler.DeleteMessage(conversationId, messageId))
            .Produces<ServiceResponse>();
        
        conversationGroup.MapPatch(
                "/{conversationId:guid}",
                async ([FromServices] IConversationHandler handler, [FromRoute] Guid conversationId, [FromBody] SetConversationSystemMessage systemMessage) =>
                await handler.SetConversationSystemMessage(conversationId, systemMessage))
            .Produces<ServiceResponse<SetSystemMessageResponseDto>>();
    }
}