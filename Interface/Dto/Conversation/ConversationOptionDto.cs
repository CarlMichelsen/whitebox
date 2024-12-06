namespace Interface.Dto.Conversation;

public record ConversationOptionDto(
    Guid Id,
    string Title,
    long LastAltered);