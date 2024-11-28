using Interface.Llm.Dto.OpenAi;
using Interface.Llm.Dto.OpenAi.Response;
using Interface.Llm.Dto.OpenAi.Response.Stream;

namespace Interface.Llm.Client;

public interface IXAiClient : ILlmClient<OpenAiPrompt, OpenAiResponse, OpenAiChunk>;