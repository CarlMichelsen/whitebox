using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic.Response;

public record AnthropicErrorContainer(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("message")] string Message);