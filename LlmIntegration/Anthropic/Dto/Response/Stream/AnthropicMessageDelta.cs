using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response.Stream;

public class AnthropicMessageDelta : BaseAnthropicEvent
{
    public override string Type => "message_delta";
    
    [JsonPropertyName("delta")]
    public required AnthropicStopInformation Delta { get; init; }
    
    [JsonPropertyName("usage")]
    public required AnthropicOutputUsage Usage { get; init; }
}