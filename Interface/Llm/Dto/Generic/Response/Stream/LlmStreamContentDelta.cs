namespace Interface.Llm.Dto.Generic.Response.Stream;

public class LlmStreamContentDelta : LlmStreamEvent
{
    public override LlmStreamChunkType Type => LlmStreamChunkType.ContentDelta;
    
    public required LlmPart Delta { get; init; }
}