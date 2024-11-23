using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response.Stream;

public class AnthropicContentBlockStop : BaseAnthropicEvent
{
    public override string Type => "content_block_stop";
    
    [JsonPropertyName("index")]
    public required int Index { get; init; }
}