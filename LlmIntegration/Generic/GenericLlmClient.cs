using LLMIntegration.Anthropic;
using LLMIntegration.Client;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.Google;
using LLMIntegration.OpenAi;
using LLMIntegration.Util;
using LLMIntegration.Validation;
using LLMIntegration.X;
using Microsoft.Extensions.DependencyInjection;

namespace LLMIntegration.Generic;

public class GenericLlmClient(
    IServiceProvider serviceProvider)
{
    private readonly LlmPromptValidator validator = new();
    
    public async Task<LlmResponse> Prompt(LlmPrompt prompt)
    {
        var validationResult = await this.validator.ValidateAsync(prompt);
        if (!validationResult.IsValid)
        {
            throw new LlmValidationException(validationResult);
        }
        
        var client = this.Create(prompt.Model.Provider);
        return await client.Prompt(prompt);
    }

    public async IAsyncEnumerable<LlmStreamEvent> StreamPrompt(LlmPrompt prompt)
    {
        var validationResult = await this.validator.ValidateAsync(prompt);
        if (!validationResult.IsValid)
        {
            yield return new LlmStreamError { Error = string.Join('\n', validationResult.Errors) };
            yield break;
        }
        
        var client = this.Create(prompt.Model.Provider);
        await foreach (var streamEvent in client.StreamPrompt(prompt))
        {
            yield return streamEvent;
        }
    }

    private IGenericLlmClient Create(LlmProvider provider)
    {
        return provider switch
        {
            LlmProvider.Anthropic => serviceProvider.GetRequiredService<GenericAnthropicClient>(),
            LlmProvider.Google => serviceProvider.GetRequiredService<GenericGoogleClient>(),
            LlmProvider.OpenAi => serviceProvider.GetRequiredService<GenericOpenAiClient>(),
            LlmProvider.X => serviceProvider.GetRequiredService<GenericXAiClient>(),
            _ => throw new NotSupportedException("Unsupported Generic LlmProvider"),
        };
    }
}