using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi.Response;

public record OpenAiResponse(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("object")] string Object,
    [property: JsonPropertyName("created")] long Created,
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("system_fingerprint")] string SystemFingerprint,
    [property: JsonPropertyName("choices")] List<OpenAiChoice> Choices,
    [property: JsonPropertyName("usage")] OpenAiUsage Usage);