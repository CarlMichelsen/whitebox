using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.Interface;
using LLMIntegration.Util;

namespace LLMIntegration.OpenAi;

public class GenericOpenAiClient(
    IOpenAiClient openAiClient) : IGenericLlmClient
{
    public LlmProvider Provider => LlmProvider.OpenAi;
    
    public async Task<LlmResponse> Prompt(LlmPrompt prompt)
    {
        var openAiPrompt = OpenAiGenericMapper.Map(prompt);
        var openAiResponse = await openAiClient.Prompt(openAiPrompt);
        return OpenAiGenericMapper.Map(openAiResponse);
    }

    public IAsyncEnumerable<LlmStreamEvent> StreamPrompt(LlmPrompt prompt)
    {
        var openAiPrompt = OpenAiGenericMapper.Map(prompt);
        return LlmStreamEventHandler.CreateAsyncEnumerable(
            openAiClient.StreamPrompt(openAiPrompt),
            new OpenAiGenericStreamMapper());
    }
}