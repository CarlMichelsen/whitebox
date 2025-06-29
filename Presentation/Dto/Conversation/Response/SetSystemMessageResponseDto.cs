using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response;

public record SetSystemMessageResponseDto(
    [property: JsonPropertyName("conversationId")]
    Guid ConversationId,
    [property: JsonPropertyName("currentSystemMessage")]
    string CurrentSystemMessage);