using System.Text.Json.Serialization;

namespace LLMIntegration.OpenAi.Dto.Response;

public record OpenAiCompletionTokensDetails(
    [property: JsonPropertyName("reasoning_tokens")] int ReasoningTokens,
    [property: JsonPropertyName("accepted_prediction_tokens")] int AcceptedPredictionTokens,
    [property: JsonPropertyName("rejected_prediction_tokens")] int RejectedPredictionTokens);