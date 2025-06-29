using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation;

public record UsageDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("inputTokens")] int InputTokens,
    [property: JsonPropertyName("outputTokens")] int OutputTokens,
    [property: JsonPropertyName("initialModelIdentifier")] string InitialModelIdentifier,
    [property: JsonPropertyName("specificModelIdentifier")] string SpecificModelIdentifier);