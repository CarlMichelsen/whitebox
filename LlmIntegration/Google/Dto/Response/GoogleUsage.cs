using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto.Response;

public record GoogleUsage(
    [property: JsonPropertyName("promptTokenCount")] int PromptTokenCount,
    [property: JsonPropertyName("candidatesTokenCount")] int? CandidatesTokenCount,
    [property: JsonPropertyName("totalTokenCount")] int TotalTokenCount);