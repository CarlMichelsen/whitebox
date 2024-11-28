using Interface.Llm;
using Interface.Llm.Client;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;
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