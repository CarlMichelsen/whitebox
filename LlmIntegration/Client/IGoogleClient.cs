using LLMIntegration.Google.Dto;
using LLMIntegration.Google.Dto.Response;
using LLMIntegration.Google.Dto.Response.Stream;

namespace LLMIntegration.Client;

public interface IGoogleClient : ILlmClient<GooglePrompt, GoogleResponse, GoogleStreamChunk>;