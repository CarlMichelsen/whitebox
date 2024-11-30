using LLMIntegration.Anthropic.Dto;
using LLMIntegration.Anthropic.Dto.Response;

namespace LLMIntegration.Interface;

public interface IAnthropicClient : ILlmClient<AnthropicPrompt, AnthropicResponse, BaseAnthropicEvent>;