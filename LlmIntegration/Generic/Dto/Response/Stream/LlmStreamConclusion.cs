using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto.Response.Stream;

public class LlmStreamConclusion : LlmStreamEvent
{
    [JsonIgnore]
    public override LlmStreamChunkType Type => LlmStreamChunkType.Conclusion;
    
    [JsonPropertyName("responseId")] 
    public required string ResponseId { get; init; }
    
    [JsonPropertyName("modelIdentifier")]
    public required string ModelIdentifier { get; init; }
    
    [JsonPropertyName("stopReason")]
    public required string StopReason { get; init; }
    
    [JsonPropertyName("usage")]
    public required LlmUsage Usage { get; init; }
}