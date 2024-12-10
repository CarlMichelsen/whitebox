using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto;

public record AnthropicPrompt(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("system"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? System,
    [property: JsonPropertyName("max_tokens")] int MaxTokens,
    [property: JsonPropertyName("messages")] List<AnthropicMessage> Messages,
    [property: JsonPropertyName("stream")] bool Stream = false);