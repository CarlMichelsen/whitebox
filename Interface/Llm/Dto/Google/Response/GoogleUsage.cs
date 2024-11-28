using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google.Response;

public record GoogleUsage(
    [property: JsonPropertyName("promptTokenCount")] int PromptTokenCount,
    [property: JsonPropertyName("candidatesTokenCount")] int CandidatesTokenCount,
    [property: JsonPropertyName("totalTokenCount")] int TotalTokenCount);