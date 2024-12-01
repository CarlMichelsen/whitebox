using Application.Mapper;
using Application.Validation.ChatConfiguration;
using Database;
using Interface.Accessor;
using Interface.Dto;
using Interface.Dto.Configuration;
using Interface.Dto.Model;
using Interface.Repository;
using Interface.Service;
using LLMIntegration.Util;
using Microsoft.Extensions.Logging;

namespace Application.Service;

public class ChatConfigurationService(
    ILogger<ChatConfigurationService> logger,
    IUserContextAccessor userContextAccessor,
    IChatConfigurationRepository chatConfigurationRepository,
    ApplicationContext applicationContext) : IChatConfigurationService
{
    public async Task<ServiceResponse<string>> SetDefaultSystemMessage(SetDefaultSystemMessageDto request)
    {
        try
        {
            var validator = new SetDefaultSystemMessageDtoValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return new ServiceResponse<string>(errors);
            }
            
            var user = userContextAccessor.GetUserContext().User;
            var chatConfiguration = await chatConfigurationRepository
                .GetOrCreateChatConfigurationAsync(user);
        
            chatConfiguration.DefaultSystemMessage = request.SystemMessage;
            await applicationContext.SaveChangesAsync();
            
            return new ServiceResponse<string>(chatConfiguration.DefaultSystemMessage);
        }
        catch (Exception e)
        {
            const string err = "Failed to set default system message";
            logger.LogCritical(e, "Error {Message}", err);

            var res = new ServiceResponse<string>();
            res.Errors.Add(err);
            return res;
        }
    }

    public async Task<ServiceResponse<LlmModelDto>> SetSelectedModelIdentifier(SetSelectedModelDto request)
    {
        try
        {
            var validator = new SetSelectedModelIdentifierDtoValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return new ServiceResponse<LlmModelDto>(errors);
            }
            
            var user = userContextAccessor.GetUserContext().User;
            var chatConfiguration = await chatConfigurationRepository
                .GetOrCreateChatConfigurationAsync(user);
        
            chatConfiguration.SelectedModelIdentifier = request.ModelIdentifier;
            await applicationContext.SaveChangesAsync();

            if (!LlmModels.TryGetModel(request.ModelIdentifier, out var model))
            {
                throw new Exception("Model identifier should already have been validated");
            }
            
            return new ServiceResponse<LlmModelDto>(AvailableModelsMapper.Map(model!));
        }
        catch (Exception e)
        {
            const string err = "Failed to select model identifier";
            logger.LogCritical(e, "Error {Message}", err);
            var res = new ServiceResponse<LlmModelDto>();
            res.Errors.Add(err);
            return res;
        }
    }

    public async Task<ServiceResponse<ChatConfigurationDto>> GetChatConfiguration()
    {
        try
        {
            var user = userContextAccessor.GetUserContext().User;
            var chatConfiguration = await chatConfigurationRepository
                .GetOrCreateChatConfigurationAsync(user);
            await applicationContext.SaveChangesAsync();
            
            var mapped = ChatConfigurationMapper.Map(chatConfiguration);
            return new ServiceResponse<ChatConfigurationDto>(mapped);
        }
        catch (Exception e)
        {
            const string err = "Failed to get chat configuration";
            logger.LogCritical(e, "Error {Message}", err);
            var res = new ServiceResponse<ChatConfigurationDto>();
            res.Errors.Add(err);
            return res;
        }
    }
}