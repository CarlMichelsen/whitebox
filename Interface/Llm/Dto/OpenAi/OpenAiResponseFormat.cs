using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi;

public record OpenAiResponseFormat(
    [property: JsonPropertyName("json_object")] string JsonObject,
    [property: JsonPropertyName("type")] string Type = "json_schema");