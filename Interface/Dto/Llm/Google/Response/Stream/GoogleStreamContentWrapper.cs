using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Google.Response.Stream;

public record GoogleStreamContentWrapper(
    [property: JsonPropertyName("content")] GoogleContent Content,
    [property: JsonPropertyName("finishReason")] string? FinishReason);