using System.Text.Json.Serialization;
using Presentation.Dto.Model;

namespace Presentation.Dto.Conversation.Response.Stream;

public class AssistantMessageEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "AssistantMessage";
    
    [JsonPropertyName("messageId")]
    public required Guid MessageId { get; init; }
    
    [JsonPropertyName("replyToMessageId")]
    public required Guid ReplyToMessageId { get; init; }
    
    [JsonPropertyName("model")]
    public required LlmModelDto Model { get; init; }
}