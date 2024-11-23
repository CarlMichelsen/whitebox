using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response;

public record AnthropicUsage(
    [property: JsonPropertyName("input_tokens")] int InputTokens,
    [property: JsonPropertyName("output_tokens")] int OutputTokens);