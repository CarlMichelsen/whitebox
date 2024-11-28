namespace Interface.Llm.Dto.Generic.Response.Stream;

public class LlmStreamConclusion : LlmStreamEvent
{
    public override LlmStreamChunkType Type => LlmStreamChunkType.Conclusion;
    
    public required string ResponseId { get; init; }
    
    public required string ModelIdentifier { get; init; }
    
    public required string StopReason { get; init; }
    
    public required LlmUsage Usage { get; init; }
}