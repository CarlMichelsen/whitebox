using Interface.Llm.Dto.Anthropic;
using Interface.Llm.Dto.Anthropic.Response;

namespace Interface.Llm.Client;

public interface IAnthropicClient : ILlmClient<AnthropicPrompt, AnthropicResponse, BaseAnthropicEvent>;