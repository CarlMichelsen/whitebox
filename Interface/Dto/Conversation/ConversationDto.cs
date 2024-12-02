using Domain.User;

namespace Interface.Dto.Conversation;

public record ConversationDto(
    string Id,
    AuthenticatedUser Creator,
    long CreatedUtc,
    long LastAppendedUtc);