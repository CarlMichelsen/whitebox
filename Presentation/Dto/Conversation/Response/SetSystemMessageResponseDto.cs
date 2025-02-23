using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation.Response;

public record SetSystemMessageResponseDto(
    [property: JsonPropertyName("conversationId")]
    Guid ConversationId,
    [property: JsonPropertyName("currentSystemMessage")]
    string CurrentSystemMessage);