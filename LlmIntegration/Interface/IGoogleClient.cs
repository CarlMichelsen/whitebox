using LLMIntegration.Google.Dto;
using LLMIntegration.Google.Dto.Response;
using LLMIntegration.Google.Dto.Response.Stream;

namespace LLMIntegration.Interface;

public interface IGoogleClient : ILlmClient<GooglePrompt, GoogleResponse, GoogleStreamChunk>;