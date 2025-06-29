using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ConversationEventDto), "Conversation")]
[JsonDerivedType(typeof(SetSummaryEventDto), "SetSummary")]
[JsonDerivedType(typeof(ErrorEventDto), "Error")]
[JsonDerivedType(typeof(PingEventDto), "Ping")]
[JsonDerivedType(typeof(AssistantMessageEventDto), "AssistantMessage")]
[JsonDerivedType(typeof(AssistantMessageDeltaEventDto), "AssistantMessageDelta")]
[JsonDerivedType(typeof(AssistantUsageEventDto), "AssistantUsage")]
[JsonDerivedType(typeof(UserMessageEventDto), "UserMessage")]
public abstract class BaseStreamResponseDto
{
    [JsonPropertyName("type")]
    public abstract string Type { get; }
    
    [JsonPropertyName("conversationId")]
    public required Guid ConversationId { get; init; }
}