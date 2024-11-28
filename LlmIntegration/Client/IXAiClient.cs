using LLMIntegration.OpenAi.Dto;
using LLMIntegration.OpenAi.Dto.Response;
using LLMIntegration.OpenAi.Dto.Response.Stream;

namespace LLMIntegration.Client;

public interface IXAiClient : ILlmClient<OpenAiPrompt, OpenAiResponse, OpenAiChunk>;