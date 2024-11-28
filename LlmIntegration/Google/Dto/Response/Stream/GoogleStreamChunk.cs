using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto.Response.Stream;

public record GoogleStreamChunk(
    [property: JsonPropertyName("candidates")] List<GoogleStreamContentWrapper> Candidates,
    [property: JsonPropertyName("usageMetadata")] GoogleUsage UsageMetadata,
    [property: JsonPropertyName("modelVersion")] string ModelVersion);