using Interface.Dto.Conversation;
using Interface.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Handler;

public class ConversationHandler(
    ILogger<ConversationHandler> logger,
    IHttpContextAccessor httpContextAccessor) : IConversationHandler
{
    private const string MockResponse = "{\"type\": \"ping\", \"counter\": {{COUNT}}}\n";
    
    public async Task Append(AppendConversationDto appendConversation)
    {
        logger.LogInformation("Append {AppendConversation}", appendConversation);
        
        await Task.Delay(TimeSpan.FromSeconds(1));
        for (var i = 0; i < 150; i++)
        {
            var payload = MockResponse.Replace("{{COUNT}}", (i + 1).ToString());
            if ((i + 1) % 10 == 0)
            {
                logger.LogInformation("Sent: {Chunk}", payload.Trim());
            }
            
            await httpContextAccessor.HttpContext!.Response.WriteAsync(payload);
            await Task.Delay(TimeSpan.FromMilliseconds(1));
        }
        
        logger.LogInformation("DONE");
    }
}