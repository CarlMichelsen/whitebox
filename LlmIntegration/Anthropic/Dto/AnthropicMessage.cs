using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto;

public record AnthropicMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] List<AnthropicContent> Content);