using LLMIntegration.Client;
using LLMIntegration.OpenAi;
using LLMIntegration.OpenAi.Dto;
using LLMIntegration.OpenAi.Dto.Response;
using LLMIntegration.OpenAi.Dto.Response.Stream;

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