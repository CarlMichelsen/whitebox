using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi;

public record OpenAiStreamOptions(
    [property: JsonPropertyName("include_usage")] bool IncludeUsage);