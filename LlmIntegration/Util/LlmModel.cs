using System.Text.Json.Serialization;

namespace LLMIntegration.Util;

public record LlmModel(
    [property: JsonPropertyName("provider")] LlmProvider Provider,
    [property: JsonPropertyName("modelName")] string ModelName,
    [property: JsonPropertyName("modelDescription")] string ModelDescription,
    [property: JsonPropertyName("modelIdentifier")] string ModelIdentifier,
    [property: JsonPropertyName("maxCompletionTokens")] int MaxCompletionTokens = 2048,
    [property: JsonPropertyName("legacy")] bool IsLegacy = false);