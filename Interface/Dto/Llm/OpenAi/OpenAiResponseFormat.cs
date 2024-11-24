using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi;

public record OpenAiResponseFormat(
    [property: JsonPropertyName("json_object")] string JsonObject,
    [property: JsonPropertyName("type")] string Type = "json_schema");