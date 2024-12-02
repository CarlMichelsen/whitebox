namespace Interface.Dto.Conversation;

public record MessageContentDto(
    string Id,
    string Type,
    string Value,
    int SortOrder);