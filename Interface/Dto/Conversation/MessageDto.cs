using Interface.Dto.Model;

namespace Interface.Dto.Conversation;

public record MessageDto(
    string Id,
    string PreviousMessageId,
    LlmModelDto? AiModel,
    List<MessageContentDto> Content,
    long CreatedUtc);