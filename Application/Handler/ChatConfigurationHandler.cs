using Microsoft.AspNetCore.Http;
using Presentation.Dto.Configuration;
using Presentation.Handler;
using Presentation.Service;

namespace Application.Handler;

public class ChatConfigurationHandler(
    IChatConfigurationService chatConfigurationService) : IChatConfigurationHandler
{
    public async Task<IResult> SetDefaultSystemMessage(SetDefaultSystemMessageDto request)
    {
        var res = await chatConfigurationService.SetDefaultSystemMessage(request);
        return Results.Ok(res);
    }

    public async Task<IResult> SetSelectedModelIdentifier(SetSelectedModelDto request)
    {
        var res = await chatConfigurationService.SetSelectedModelIdentifier(request);
        return Results.Ok(res);
    }

    public async Task<IResult> GetChatConfiguration()
    {
        var res = await chatConfigurationService.GetChatConfiguration();
        return Results.Ok(res);
    }
}