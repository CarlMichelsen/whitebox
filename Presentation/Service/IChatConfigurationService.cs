using Presentation.Dto;
using Presentation.Dto.Configuration;
using Presentation.Dto.Model;

namespace Presentation.Service;

public interface IChatConfigurationService
{
    Task<ServiceResponse<string>> SetDefaultSystemMessage(SetDefaultSystemMessageDto request);
    
    Task<ServiceResponse<LlmModelDto>> SetSelectedModelIdentifier(SetSelectedModelDto request);
    
    Task<ServiceResponse<ChatConfigurationDto>> GetChatConfiguration();
}