using LLMIntegration.Client;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.Util;

namespace LLMIntegration.Anthropic;

public class GenericAnthropicClient(
    IAnthropicClient anthropicClient) : IGenericLlmClient
{
    public LlmProvider Provider => LlmProvider.Anthropic;
    
    public async Task<LlmResponse> Prompt(LlmPrompt prompt)
    {
        var anthropicPrompt = AnthropicGenericMapper.Map(prompt);
        var anthropicResponse = await anthropicClient.Prompt(anthropicPrompt);
        return AnthropicGenericMapper.Map(anthropicResponse);
    }

    public IAsyncEnumerable<LlmStreamEvent> StreamPrompt(LlmPrompt prompt)
    {
        var anthropicPrompt = AnthropicGenericMapper.Map(prompt);
        return LlmStreamEventHandler.CreateAsyncEnumerable(
            anthropicClient.StreamPrompt(anthropicPrompt),
            new AnthropicGenericStreamMapper());
    }
}