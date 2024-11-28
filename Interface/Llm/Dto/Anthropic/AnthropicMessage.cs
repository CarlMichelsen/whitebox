using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic;

public record AnthropicMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] List<AnthropicContent> Content);