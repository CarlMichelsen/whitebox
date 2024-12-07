using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation.Response.Stream;

public class AssistantMessageEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "AssistantMessage";
    
    [JsonPropertyName("messageId")]
    public required Guid MessageId { get; init; }
}