using System.Text.Json.Serialization;
using Presentation.Dto.Model;

namespace Presentation.Dto.Conversation;

public record MessageDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("conversationId")] Guid ConversationId,
    [property: JsonPropertyName("previousMessageId")] Guid? PreviousMessageId,
    [property: JsonPropertyName("aiModel")] LlmModelDto? AiModel,
    [property: JsonPropertyName("content")] List<MessageContentDto> Content,
    [property: JsonPropertyName("usage")] UsageDto? Usage,
    [property: JsonPropertyName("createdUtc")] long CreatedUtc);