using System.Text.Json.Serialization;
using LLMIntegration.Util;

namespace LLMIntegration.Generic.Dto;

public record LlmPrompt(
    [property: JsonPropertyName("model")] LlmModel Model,
    [property: JsonPropertyName("content")] LlmContent Content,
    [property: JsonPropertyName("maxTokens")] int MaxTokens);