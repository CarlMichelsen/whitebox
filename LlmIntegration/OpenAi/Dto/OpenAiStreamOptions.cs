using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto;

public record OpenAiStreamOptions(
    [property: JsonPropertyName("include_usage")] bool IncludeUsage);