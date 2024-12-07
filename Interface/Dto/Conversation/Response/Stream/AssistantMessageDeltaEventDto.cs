using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation.Response.Stream;

public class AssistantMessageDeltaEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "AssistantMessageDelta";
    
    [JsonPropertyName("messageId")]
    public required Guid MessageId { get; init; }
    
    [JsonPropertyName("contentDelta")]
    public required string ContentDelta { get; init; }
}