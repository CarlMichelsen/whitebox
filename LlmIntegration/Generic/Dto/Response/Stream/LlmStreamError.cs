namespace LLMIntegration.Generic.Dto.Response.Stream;

public class LlmStreamError : LlmStreamEvent
{
    public override LlmStreamChunkType Type => LlmStreamChunkType.Error;
    
    public required string Error { get; init; }
}