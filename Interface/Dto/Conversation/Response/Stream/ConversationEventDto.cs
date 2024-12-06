namespace Interface.Dto.Conversation.Response.Stream;

public class ConversationEventDto : BaseStreamResponseDto
{
    public override string Type => "Conversation";
    
    public required Guid ConversationId { get; init; }
}