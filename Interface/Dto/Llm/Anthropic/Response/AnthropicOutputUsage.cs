using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response;

public record AnthropicOutputUsage(
    [property: JsonPropertyName("output_tokens")] int OutputTokens);