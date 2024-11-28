using Interface.Llm;
using Interface.Llm.Client;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;
using LLMIntegration.Anthropic;
using LLMIntegration.Exception;
using LLMIntegration.Google;
using LLMIntegration.OpenAi;
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

    public async IAsyncEnumerable<LlmStreamChunk> StreamPrompt(LlmPrompt prompt)
    {
        var validationResult = await this.validator.ValidateAsync(prompt);
        if (!validationResult.IsValid)
        {
            throw new LlmValidationException(validationResult);
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
            _ => throw new NotSupportedException("Unsupported LlmProvider"),
        };
    }
}