namespace Interface.Dto.Conversation.Response.Stream;

public class PingEventDto : BaseStreamResponseDto
{
    public override string Type => "Ping";
}