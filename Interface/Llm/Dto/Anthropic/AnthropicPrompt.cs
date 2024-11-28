using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic;

public record AnthropicPrompt(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("system")] string? System,
    [property: JsonPropertyName("max_tokens")] int MaxTokens,
    [property: JsonPropertyName("messages")] List<AnthropicMessage> Messages,
    [property: JsonPropertyName("stream")] bool Stream = false);