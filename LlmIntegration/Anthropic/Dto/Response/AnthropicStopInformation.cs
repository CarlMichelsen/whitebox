using System.Text.Json.Serialization;

namespace LLMIntegration.Anthropic.Dto.Response;

public record AnthropicStopInformation(
    [property: JsonPropertyName("stop_reason")] string StopReason,
    [property: JsonPropertyName("stop_sequence")] string? StopSequence);