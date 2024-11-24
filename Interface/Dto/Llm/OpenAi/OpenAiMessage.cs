using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi;

public record OpenAiMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] List<OpenAiContent> Content);