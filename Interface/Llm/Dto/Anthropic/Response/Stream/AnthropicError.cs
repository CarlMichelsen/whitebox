using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic.Response.Stream;

public class AnthropicError : BaseAnthropicEvent
{
    public override string Type => "error";
    
    [JsonPropertyName("error")]
    public required AnthropicErrorContainer Error { get; init; }
}