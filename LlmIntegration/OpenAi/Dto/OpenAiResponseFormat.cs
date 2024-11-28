using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto;

public record OpenAiResponseFormat(
    [property: JsonPropertyName("json_object")] string JsonObject,
    [property: JsonPropertyName("type")] string Type = "json_schema");