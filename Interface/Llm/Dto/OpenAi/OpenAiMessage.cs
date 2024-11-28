using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi;

public record OpenAiMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] List<OpenAiContent> Content);