using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

public class AssistantUsageEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "AssistantUsage";
    
    [JsonPropertyName("messageId")]
    public required Guid MessageId { get; init; }
    
    [JsonPropertyName("usage")]
    public required UsageDto Usage { get; init; }
}