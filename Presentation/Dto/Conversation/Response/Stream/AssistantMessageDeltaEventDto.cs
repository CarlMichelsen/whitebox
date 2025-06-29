using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

public class AssistantMessageDeltaEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "AssistantMessageDelta";
    
    [JsonPropertyName("messageId")]
    public required Guid MessageId { get; init; }
    
    [JsonPropertyName("contentId")]
    public required Guid ContentId { get; init; }
    
    [JsonPropertyName("contentType")]
    public required string ContentType { get; init; }
    
    [JsonPropertyName("sortOrder")]
    public required int SortOrder { get; init; }
    
    [JsonPropertyName("contentDelta")]
    public required string ContentDelta { get; init; }
}