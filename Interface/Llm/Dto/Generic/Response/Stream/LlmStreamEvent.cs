namespace Interface.Llm.Dto.Generic.Response.Stream;

public abstract class LlmStreamEvent
{
    public abstract LlmStreamChunkType Type { get; }
}