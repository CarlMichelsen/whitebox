namespace LLMIntegration.Generic.Dto.Response.Stream;

public class LlmStreamContentDelta : LlmStreamEvent
{
    public override LlmStreamChunkType Type => LlmStreamChunkType.ContentDelta;
    
    public required LlmPart Delta { get; init; }
}