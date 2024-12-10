using System.Text.Json.Serialization;
using Domain.User;

namespace Interface.Dto.Conversation;

public record ConversationDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("creator")] AuthenticatedUser Creator,
    [property: JsonPropertyName("systemMessage")] string? SystemMessage,
    [property: JsonPropertyName("summary")] string? Summary,
    [property: JsonPropertyName("sections")] List<ConversationSectionDto> Sections,
    [property: JsonPropertyName("createdUtc")] long CreatedUtc,
    [property: JsonPropertyName("lastAlteredUtc")] long LastAlteredUtc);