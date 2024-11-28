using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic.Response;

public record AnthropicUsage(
    [property: JsonPropertyName("input_tokens")] int InputTokens,
    [property: JsonPropertyName("output_tokens")] int OutputTokens);