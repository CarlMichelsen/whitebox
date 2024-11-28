using Interface.Llm;
using Interface.Llm.Client;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;
using Interface.Llm.Dto.OpenAi.Response.Stream;
using LLMIntegration.OpenAi;
using LLMIntegration.Util;

namespace LLMIntegration.X;

public class GenericXAiClient(
    IXAiClient xAiClient) : IGenericLlmClient
{
    public LlmProvider Provider => LlmProvider.X; 
    
    public async Task<LlmResponse> Prompt(LlmPrompt prompt)
    {
        // X-AI is using the OpenAi api models.
        var openAiPrompt = OpenAiGenericMapper.Map(prompt);
        var openAiResponse = await xAiClient.Prompt(openAiPrompt);
        return OpenAiGenericMapper.Map(openAiResponse);
    }

    public IAsyncEnumerable<LlmStreamEvent> StreamPrompt(LlmPrompt prompt)
    {
        var openAiPrompt = OpenAiGenericMapper.Map(prompt);
        return LlmStreamEventHandler.CreateAsyncEnumerable(
            xAiClient.StreamPrompt(openAiPrompt),
            new OpenAiGenericStreamMapper());
    }
}