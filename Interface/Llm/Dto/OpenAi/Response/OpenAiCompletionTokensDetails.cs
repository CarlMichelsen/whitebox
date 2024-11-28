using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi.Response;

public record OpenAiCompletionTokensDetails(
    [property: JsonPropertyName("reasoning_tokens")] int ReasoningTokens,
    [property: JsonPropertyName("accepted_prediction_tokens")] int AcceptedPredictionTokens,
    [property: JsonPropertyName("rejected_prediction_tokens")] int RejectedPredictionTokens);