using LLMIntegration.Anthropic.Dto;
using LLMIntegration.Anthropic.Dto.Response;

namespace LLMIntegration.Client;

public interface IAnthropicClient : ILlmClient<AnthropicPrompt, AnthropicResponse, BaseAnthropicEvent>;