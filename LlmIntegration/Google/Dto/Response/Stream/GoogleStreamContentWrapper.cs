using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto.Response.Stream;

public record GoogleStreamContentWrapper(
    [property: JsonPropertyName("content")] GoogleContent Content,
    [property: JsonPropertyName("finishReason")] string? FinishReason);