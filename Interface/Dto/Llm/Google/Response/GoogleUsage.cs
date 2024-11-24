using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Google.Response;

public record GoogleUsage(
    [property: JsonPropertyName("promptTokenCount")] int PromptTokenCount,
    [property: JsonPropertyName("candidatesTokenCount")] int CandidatesTokenCount,
    [property: JsonPropertyName("totalTokenCount")] int TotalTokenCount);