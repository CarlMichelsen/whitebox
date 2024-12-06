namespace Interface.Dto.Conversation.Response.Stream;

public class UserMessageEventDto : BaseStreamResponseDto
{
    public override string Type => "UserMessage";
    
    public required MessageDto Message { get; init; }
}