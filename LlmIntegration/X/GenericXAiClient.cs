using Interface.Llm;
using Interface.Llm.Client;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;

namespace LLMIntegration.X;

public class GenericXAiClient : IGenericLlmClient
{
    public LlmProvider Provider => LlmProvider.X; 
    
    public async Task<LlmResponse> Prompt(LlmPrompt prompt)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<LlmStreamChunk> StreamPrompt(LlmPrompt prompt)
    {
        throw new NotImplementedException();
    }
}