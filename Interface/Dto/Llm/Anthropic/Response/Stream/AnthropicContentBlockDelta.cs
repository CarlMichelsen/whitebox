using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response.Stream;

public class AnthropicContentBlockDelta : BaseAnthropicEvent
{
    public override string Type => "content_block_delta";
    
    [JsonPropertyName("index")]
    public required int Index { get; init; }
    
    [JsonPropertyName("delta")]
    public required AnthropicContent Delta { get; init; }
}