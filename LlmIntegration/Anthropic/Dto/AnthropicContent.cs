using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto;

public record AnthropicContent(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("type")] string Type);