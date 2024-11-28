using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response.Stream;

public class AnthropicContentBlockStart : BaseAnthropicEvent
{
    public override string Type => "content_block_start";
    
    [JsonPropertyName("index")]
    public required int Index { get; init; }
    
    [JsonPropertyName("content_block")]
    public required AnthropicContent ContentBlock { get; init; }
}