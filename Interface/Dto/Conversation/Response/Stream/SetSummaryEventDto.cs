using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation.Response.Stream;

public class SetSummaryEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "SetSummary";
    
    [JsonPropertyName("conversationId")]
    public required Guid ConversationId { get; init; }
    
    [JsonPropertyName("summary")]
    public required string Summary { get; init; }
}