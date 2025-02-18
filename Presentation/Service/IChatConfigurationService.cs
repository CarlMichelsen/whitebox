using Interface.Dto;
using Interface.Dto.Configuration;
using Interface.Dto.Model;

namespace Interface.Service;

public interface IChatConfigurationService
{
    Task<ServiceResponse<string>> SetDefaultSystemMessage(SetDefaultSystemMessageDto request);
    
    Task<ServiceResponse<LlmModelDto>> SetSelectedModelIdentifier(SetSelectedModelDto request);
    
    Task<ServiceResponse<ChatConfigurationDto>> GetChatConfiguration();
}