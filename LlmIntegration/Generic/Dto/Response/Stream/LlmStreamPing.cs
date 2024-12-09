using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto.Response.Stream;

public class LlmStreamPing : LlmStreamEvent
{
    [JsonIgnore]
    public override LlmStreamChunkType Type => LlmStreamChunkType.Ping;
}