using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.Util;

namespace LLMIntegration.Interface;

public interface IGenericLlmClient : ILlmClient<LlmPrompt, LlmResponse, LlmStreamEvent>
{
    LlmProvider Provider { get; }
}