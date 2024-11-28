using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response;

public record AnthropicOutputUsage(
    [property: JsonPropertyName("output_tokens")] int OutputTokens);