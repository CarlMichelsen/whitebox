using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response;

public record AnthropicErrorContainer(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("message")] string Message);