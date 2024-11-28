using Interface.Llm;
using Interface.Llm.Client;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;

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

    public IAsyncEnumerable<LlmStreamChunk> StreamPrompt(LlmPrompt prompt)
    {
        var mapper = new AnthropicGenericStreamMapper();
        throw new NotImplementedException();
    }
}