﻿using System.Text.Json.Serialization;

namespace Interface.Dto.Conversation.Response.Stream;

public class ConversationEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "Conversation";
    
    [JsonPropertyName("conversationId")]
    public required Guid ConversationId { get; init; }
}