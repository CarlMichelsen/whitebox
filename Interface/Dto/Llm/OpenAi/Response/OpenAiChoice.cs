using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi.Response;

public record OpenAiChoice(
    [property: JsonPropertyName("index")] int Index,
    [property: JsonPropertyName("message")] OpenAiMessage Message,
    [property: JsonPropertyName("logprobs")] object? Logprobs,
    [property: JsonPropertyName("finish_reason")] string FinishReason);