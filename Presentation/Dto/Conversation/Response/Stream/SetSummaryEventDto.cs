using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

public class SetSummaryEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "SetSummary";
    
    [JsonPropertyName("summary")]
    public required string Summary { get; init; }
}