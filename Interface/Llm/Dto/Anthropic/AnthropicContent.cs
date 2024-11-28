using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic;

public record AnthropicContent(
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("type")] string Type);