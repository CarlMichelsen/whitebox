using Interface.Llm;
using Interface.Llm.Client;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;

namespace LLMIntegration.Google;

public class GenericGoogleClient(
    IGoogleClient googleClient) : IGenericLlmClient
{
    public LlmProvider Provider => LlmProvider.Google;
    
    public async Task<LlmResponse> Prompt(LlmPrompt prompt)
    {
        var googlePrompt = GoogleGenericMapper.Map(prompt);
        var googleResponse = await googleClient.Prompt(googlePrompt);
        var responseId = Guid.NewGuid().ToString();
        return GoogleGenericMapper.Map(googleResponse, responseId);
    }

    public IAsyncEnumerable<LlmStreamChunk> StreamPrompt(LlmPrompt prompt)
    {
        throw new NotImplementedException();
    }
}