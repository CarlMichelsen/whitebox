using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi.Response;

public record OpenAiUsage(
    [property: JsonPropertyName("prompt_tokens")] int PromptTokens,
    [property: JsonPropertyName("completion_tokens")] int CompletionTokens,
    [property: JsonPropertyName("total_tokens")] int TotalTokens,
    [property: JsonPropertyName("completion_tokens_details")] OpenAiCompletionTokensDetails CompletionTokensDetails);