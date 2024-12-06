using System.Text.Json;
using Database.Entity.Id;
using Domain.Conversation.Action;
using Interface.Dto.Conversation.Request;
using Interface.Dto.Conversation.Response.Stream;
using Interface.Handler;
using Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handler;

public class ConversationStreamHandler(
    ILogger<ConversationStreamHandler> logger,
    IHttpContextAccessor httpContextAccessor,
    IConversationStreamService conversationStreamService) : IConversationStreamHandler
{
    public async Task Append(AppendConversationDto appendConversation)
    {
        try
        {
            logger.LogInformation("Append {AppendConversation}", appendConversation);
            var appendModel = Map(appendConversation);
            await foreach (var streamEvent in conversationStreamService.GetConversationResponse(appendModel))
            {
                var json = JsonSerializer.Serialize<object>(streamEvent);
                logger.LogInformation("Write {Json}", json);
                await httpContextAccessor.HttpContext!.Response.WriteAsync(json + '\n');
            }
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "Exception occured during Append");
            var err = new ErrorEventDto
            {
                Error = "Exception",
            };
            var json = JsonSerializer.Serialize<object>(err);
            await httpContextAccessor.HttpContext!.Response.WriteAsync(json);
        }
    }

    private static AppendConversation Map(AppendConversationDto appendConversationDto)
    {
        ReplyTo? replyTo = null;
        if (appendConversationDto.ReplyTo is not null)
        {
            replyTo = new ReplyTo(
                new ConversationEntityId(appendConversationDto.ReplyTo.ConversationId),
                new MessageEntityId(appendConversationDto.ReplyTo.ReplyToMessageId));
        }
        
        return new AppendConversation(
            ReplyTo: replyTo,
            Text: appendConversationDto.Text);
    }
}