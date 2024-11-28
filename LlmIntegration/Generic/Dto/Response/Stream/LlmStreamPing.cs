namespace LLMIntegration.Generic.Dto.Response.Stream;

public class LlmStreamPing : LlmStreamEvent
{
    public override LlmStreamChunkType Type => LlmStreamChunkType.Ping;
}