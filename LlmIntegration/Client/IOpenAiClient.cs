using LLMIntegration.OpenAi.Dto;
using LLMIntegration.OpenAi.Dto.Response;
using LLMIntegration.OpenAi.Dto.Response.Stream;

namespace LLMIntegration.Client;

public interface IOpenAiClient : ILlmClient<OpenAiPrompt, OpenAiResponse, OpenAiChunk>;