namespace LLMIntegration.Generic.Dto.Response.Stream;

public abstract class LlmStreamEvent
{
    public abstract LlmStreamChunkType Type { get; }
}