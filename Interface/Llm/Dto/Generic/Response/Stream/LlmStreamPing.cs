namespace Interface.Llm.Dto.Generic.Response.Stream;

public class LlmStreamPing : LlmStreamEvent
{
    public override LlmStreamChunkType Type => LlmStreamChunkType.Ping;
}