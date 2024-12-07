using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation.Response.Stream;

public class PingEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "Ping";
}