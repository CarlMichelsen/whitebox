using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response.Stream;

public class AnthropicContentBlockStop : BaseAnthropicEvent
{
    public override string Type => "content_block_stop";
    
    [JsonPropertyName("index")]
    public required int Index { get; init; }
}