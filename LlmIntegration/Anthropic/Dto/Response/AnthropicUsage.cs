using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response;

public record AnthropicUsage(
    [property: JsonPropertyName("input_tokens")] int InputTokens,
    [property: JsonPropertyName("output_tokens")] int OutputTokens);