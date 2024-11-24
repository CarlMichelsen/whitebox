using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Google.Response;

public record GoogleResponse(
    [property: JsonPropertyName("candidates")] List<GoogleContent> Candidates,
    [property: JsonPropertyName("usageMetadata")] GoogleUsage UsageMetadata,
    [property: JsonPropertyName("modelVersion")] string ModelVersion);