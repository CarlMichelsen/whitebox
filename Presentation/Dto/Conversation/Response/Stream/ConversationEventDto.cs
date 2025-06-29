using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

public class ConversationEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "Conversation";
}