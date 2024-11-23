using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic;

public record AnthropicMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] List<AnthropicContent> Content);