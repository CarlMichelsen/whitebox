using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;
using Presentation.Dto.Configuration;
using Presentation.Dto.Model;
using Presentation.Handler;

namespace Api.Endpoints;

public static class ChatConfigurationEndpoints
{
    public static void RegisterChatConfigurationEndpoints(
        this IEndpointRouteBuilder apiGroup)
    {
        var conversationGroup = apiGroup
            .MapGroup("chatConfiguration")
            .WithTags("ChatConfiguration");
        
        conversationGroup.MapGet(
                "/",
                async ([FromServices] IChatConfigurationHandler handler) =>
                await handler.GetChatConfiguration())
            .Produces<ServiceResponse<ChatConfigurationDto>>();
        
        conversationGroup.MapPost(
            "/system",
            async ([FromServices] IChatConfigurationHandler handler, [FromBody] SetDefaultSystemMessageDto dto) =>
                await handler.SetDefaultSystemMessage(dto))
                .Produces<ServiceResponse<string>>();
        
        conversationGroup.MapPost(
                "/model",
                async ([FromServices] IChatConfigurationHandler handler, [FromBody] SetSelectedModelDto dto) =>
                await handler.SetSelectedModelIdentifier(dto))
            .Produces<ServiceResponse<LlmModelDto>>();
    }
}