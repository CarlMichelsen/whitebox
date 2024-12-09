using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto.Response.Stream;

public class LlmStreamContentDelta : LlmStreamEvent
{
    [JsonIgnore]
    public override LlmStreamChunkType Type => LlmStreamChunkType.ContentDelta;
    
    [JsonPropertyName("delta")]
    public required LlmPart Delta { get; init; }
}