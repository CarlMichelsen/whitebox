using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation.Request;

public record SetConversationSystemMessage(
    [property: JsonPropertyName("systemMessage")]
    string SystemMessage);