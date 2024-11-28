using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google.Response.Stream;

public record GoogleStreamContentWrapper(
    [property: JsonPropertyName("content")] GoogleContent Content,
    [property: JsonPropertyName("finishReason")] string? FinishReason);