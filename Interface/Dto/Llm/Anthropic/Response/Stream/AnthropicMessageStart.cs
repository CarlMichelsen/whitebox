using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response.Stream;

public class AnthropicMessageStart : BaseAnthropicEvent
{
    public override string Type => "message_start";
    
    [JsonPropertyName("message")]
    public required AnthropicResponse Message { get; init; }
}