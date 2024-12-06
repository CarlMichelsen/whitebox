namespace Interface.Dto.Conversation.Response.Stream;

public class ErrorEventDto : BaseStreamResponseDto
{
    public override string Type => "Error";
    
    public required string Error { get; init; }
}