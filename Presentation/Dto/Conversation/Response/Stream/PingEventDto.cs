using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

public class PingEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "Ping";
}