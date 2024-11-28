using Interface.Llm.Client;
using Interface.Llm.Dto.OpenAi;
using Interface.Llm.Dto.OpenAi.Response;
using Interface.Llm.Dto.OpenAi.Response.Stream;
using LLMIntegration.OpenAi;

namespace LLMIntegration.X;

// https://docs.x.ai/api#making-requests
public class XAiClient(
    HttpClient httpClient) : IXAiClient
{
    private readonly OpenAiClient openAiClient = new(httpClient);
    
    public Task<OpenAiResponse> Prompt(OpenAiPrompt prompt)
    {
        return this.openAiClient.Prompt(prompt);
    }

    public IAsyncEnumerable<OpenAiChunk> StreamPrompt(OpenAiPrompt prompt)
    {
        return this.openAiClient.StreamPrompt(prompt);
    }
}