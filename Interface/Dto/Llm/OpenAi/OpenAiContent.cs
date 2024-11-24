using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi;

public record OpenAiContent(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("text")] string Text);