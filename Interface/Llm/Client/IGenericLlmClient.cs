using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;

namespace Interface.Llm.Client;

public interface IGenericLlmClient : ILlmClient<LlmPrompt, LlmResponse, LlmStreamChunk>
{
    LlmProvider Provider { get; }
}