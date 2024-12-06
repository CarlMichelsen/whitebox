using Interface.Dto.Model;

namespace Interface.Dto.Conversation;

public record MessageDto(
    Guid Id,
    Guid? PreviousMessageId,
    LlmModelDto? AiModel,
    List<MessageContentDto> Content,
    long CreatedUtc);