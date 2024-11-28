using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google.Response;

public record GoogleResponse(
    [property: JsonPropertyName("candidates")] List<GoogleCandidate> Candidates,
    [property: JsonPropertyName("usageMetadata")] GoogleUsage UsageMetadata,
    [property: JsonPropertyName("modelVersion")] string ModelVersion);