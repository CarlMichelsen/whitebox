using Interface.Dto.Configuration;
using Microsoft.AspNetCore.Http;

namespace Interface.Handler;

public interface IChatConfigurationHandler
{
    Task<IResult> SetDefaultSystemMessage(SetDefaultSystemMessageDto request);
    
    Task<IResult> SetSelectedModelIdentifier(SetSelectedModelDto request);
    
    Task<IResult> GetChatConfiguration();
}