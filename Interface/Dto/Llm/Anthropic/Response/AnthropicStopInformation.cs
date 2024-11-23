using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response;

public record AnthropicStopInformation(
    [property: JsonPropertyName("stop_reason")] string StopReason,
    [property: JsonPropertyName("stop_sequence")] string? StopSequence);