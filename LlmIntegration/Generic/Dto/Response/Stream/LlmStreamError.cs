using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto.Response.Stream;

public class LlmStreamError : LlmStreamEvent
{
    [JsonIgnore]
    public override LlmStreamChunkType Type => LlmStreamChunkType.Error;
    
    [JsonPropertyName("error")]
    public required string Error { get; init; }
}