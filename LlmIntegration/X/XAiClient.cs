using Interface.Client;
using Interface.Dto.Llm.OpenAi;
using Interface.Dto.Llm.OpenAi.Response;
using Interface.Dto.Llm.OpenAi.Response.Stream;
using LLMIntegration.OpenAi;

namespace LLMIntegration.X;

// https://docs.x.ai/api#making-requests
public class XAiClient(
    HttpClient httpClient) : ILlmClient<OpenAiPrompt, OpenAiResponse, OpenAiChunk>
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