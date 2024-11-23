using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.Anthropic.Response;

public record AnthropicResponse(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("content")] List<AnthropicContent> Content,
    [property: JsonPropertyName("stop_reason")] string StopReason,
    [property: JsonPropertyName("stop_sequence")] string? StopSequence,
    [property: JsonPropertyName("usage")] AnthropicUsage Usage);