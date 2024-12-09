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
            var pingJson = JsonSerializer.Serialize<BaseStreamResponseDto>(new PingEventDto
                { ConversationId = Guid.Empty });
            await httpContextAccessor.HttpContext!.Response.WriteAsync(pingJson + '\n');
            
            var appendModel = Map(appendConversation);
            await conversationStreamService.GetConversationResponse(appendModel, async streamEvent =>
            {
                var json = JsonSerializer.Serialize(streamEvent);
                await httpContextAccessor.HttpContext!.Response.WriteAsync(json + '\n');
            });
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "Exception occured during Append");
            var err = new ErrorEventDto
            {
                ConversationId = Guid.Empty,
                Error = "Exception",
            };
            var json = JsonSerializer.Serialize<BaseStreamResponseDto>(err);
            await httpContextAccessor.HttpContext!.Response.WriteAsync(json);
        }
    }

    private static AppendConversation Map(AppendConversationDto appendConversationDto)
    {
        ReplyTo? replyTo = null;
        if (appendConversationDto.ReplyTo is not null)
        {
            var replyToMessageId = appendConversationDto.ReplyTo.ReplyToMessageId is null
                ? null
                : new MessageEntityId((Guid)appendConversationDto.ReplyTo.ReplyToMessageId);
            replyTo = new ReplyTo(
                new ConversationEntityId(appendConversationDto.ReplyTo.ConversationId),
                replyToMessageId);
        }
        
        return new AppendConversation(
            ReplyTo: replyTo,
            Text: appendConversationDto.Text);
    }
}