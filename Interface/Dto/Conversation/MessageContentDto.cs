namespace Interface.Dto.Conversation;

public record MessageContentDto(
    Guid Id,
    string Type,
    string Value,
    int SortOrder);