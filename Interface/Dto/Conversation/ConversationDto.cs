using Domain.User;

namespace Interface.Dto.Conversation;

public record ConversationDto(
    Guid Id,
    AuthenticatedUser Creator,
    string SystemMessage,
    string? Summary,
    List<ConversationSectionDto> Sections,
    long CreatedUtc,
    long LastAlteredUtc);