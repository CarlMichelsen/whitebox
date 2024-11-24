using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response.Stream;

public class AnthropicError : BaseAnthropicEvent
{
    public override string Type => "error";
    
    [JsonPropertyName("error")]
    public required int Error { get; init; }
}