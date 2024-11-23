using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic;

public record AnthropicContent(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("type")] string Type);