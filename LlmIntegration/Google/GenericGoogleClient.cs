using LLMIntegration.Client;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.Util;

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

    public IAsyncEnumerable<LlmStreamEvent> StreamPrompt(LlmPrompt prompt)
    {
        var googlePrompt = GoogleGenericMapper.Map(prompt);
        return LlmStreamEventHandler.CreateAsyncEnumerable(
            googleClient.StreamPrompt(googlePrompt),
            new GoogleGenericStreamMapper());
    }
}