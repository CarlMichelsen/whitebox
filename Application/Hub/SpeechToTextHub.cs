using Domain.Dto.Hub;
using Domain.User;
using Interface.Accessor;
using Interface.Hub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.Hub;

[Authorize]
public class SpeechToTextHub(
    ILogger<SpeechToTextHub> logger,
    IUserContextAccessor userContextAccessor) : Hub<ISpeechToTextClientMethods>, ISpeechToTextServerMethods
{
    private UserContext UserContext => (UserContext)this.Context.Items["userContext"]!;
    
    public override async Task OnConnectedAsync()
    {
        try
        {
            await base.OnConnectedAsync();
            var userContext = userContextAccessor.GetUserContext(this.Context.GetHttpContext()!);
            this.Context.Items["userContext"] = userContext;
            logger.LogInformation(
                "{Username}<{UserId}> Connected to SpeechToTextHub",
                this.UserContext.User.Username,
                this.UserContext.User.Id);
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "{HubName} OnConnectedAsync", nameof(SpeechToTextHub));
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            await base.OnDisconnectedAsync(exception);
            if (exception is null)
            {
                logger.LogInformation(
                    "{Username}<{UserId}> Disconnected from SpeechToTextHub",
                    this.UserContext.User.Username,
                    this.UserContext.User.Id);
            }
            else
            {
                logger.LogWarning(
                    "{Username}<{UserId}> Disconnected from SpeechToTextHub because of an error {Exception}",
                    this.UserContext.User.Username,
                    this.UserContext.User.Id,
                    exception);
            }
        }
        catch (Exception e)
        {
            logger.LogCritical(e, "{HubName} OnDisconnectedAsync", nameof(SpeechToTextHub));
        }
    }
    
    public Task SendBlob(SpeechBlob speechBlob)
    {
        // Handle the received blob here
        // For example, save it to a file or database
        logger.LogInformation(
            "Received blob <{Identifier}> {Length}",
            speechBlob.Identifier,
            speechBlob.Data.Length);

        return Task.CompletedTask;
    }
}