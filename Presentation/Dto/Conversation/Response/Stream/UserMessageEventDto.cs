using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

public class UserMessageEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "UserMessage";
    
    [JsonPropertyName("message")]
    public required MessageDto Message { get; init; }
}