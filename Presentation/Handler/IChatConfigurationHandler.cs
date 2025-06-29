using Microsoft.AspNetCore.Http;
using Presentation.Dto.Configuration;

namespace Presentation.Handler;

public interface IChatConfigurationHandler
{
    Task<IResult> SetDefaultSystemMessage(SetDefaultSystemMessageDto request);
    
    Task<IResult> SetSelectedModelIdentifier(SetSelectedModelDto request);
    
    Task<IResult> GetChatConfiguration();
}