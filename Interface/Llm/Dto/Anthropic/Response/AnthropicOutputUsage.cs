using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic.Response;

public record AnthropicOutputUsage(
    [property: JsonPropertyName("output_tokens")] int OutputTokens);