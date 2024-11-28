using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Anthropic.Response;

public record AnthropicStopInformation(
    [property: JsonPropertyName("stop_reason")] string StopReason,
    [property: JsonPropertyName("stop_sequence")] string? StopSequence);