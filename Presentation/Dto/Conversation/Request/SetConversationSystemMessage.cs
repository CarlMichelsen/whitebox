using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Request;

public record SetConversationSystemMessage(
    [property: JsonPropertyName("systemMessage")]
    string SystemMessage);