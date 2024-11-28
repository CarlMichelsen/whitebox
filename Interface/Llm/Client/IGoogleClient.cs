using Interface.Llm.Dto.Google;
using Interface.Llm.Dto.Google.Response;
using Interface.Llm.Dto.Google.Response.Stream;

namespace Interface.Llm.Client;

public interface IGoogleClient : ILlmClient<GooglePrompt, GoogleResponse, GoogleStreamChunk>;