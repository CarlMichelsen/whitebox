using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response.Stream;

public class AnthropicError : BaseAnthropicEvent
{
    public override string Type => "error";
    
    [JsonPropertyName("error")]
    public required AnthropicErrorContainer Error { get; init; }
}