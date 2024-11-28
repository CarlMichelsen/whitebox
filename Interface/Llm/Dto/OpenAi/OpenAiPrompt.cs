using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi;

public record OpenAiPrompt(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("messages")] List<OpenAiMessage> Messages,
    [property: JsonPropertyName("max_tokens")] int MaxTokens,
    [property: JsonPropertyName("response_format")] OpenAiResponseFormat? ResponseFormat = null,
    [property: JsonPropertyName("stream")] bool Stream = false,
    [property: JsonPropertyName("store")] bool AllowOpenAiToStoreConversation = false);