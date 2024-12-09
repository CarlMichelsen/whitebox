using System.Text.Json.Serialization;

namespace LLMIntegration.Generic.Dto.Response.Stream;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(LlmStreamConclusion), (int)LlmStreamChunkType.Conclusion)]
[JsonDerivedType(typeof(LlmStreamContentDelta), (int)LlmStreamChunkType.ContentDelta)]
[JsonDerivedType(typeof(LlmStreamError), (int)LlmStreamChunkType.Error)]
[JsonDerivedType(typeof(LlmStreamPing), (int)LlmStreamChunkType.Ping)]
public abstract class LlmStreamEvent
{
    [JsonPropertyName("type")]
    public abstract LlmStreamChunkType Type { get; }
}