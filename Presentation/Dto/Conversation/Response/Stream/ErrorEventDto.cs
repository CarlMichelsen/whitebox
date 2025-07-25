﻿using System.Text.Json.Serialization;

namespace Presentation.Dto.Conversation.Response.Stream;

public class ErrorEventDto : BaseStreamResponseDto
{
    [JsonIgnore]
    public override string Type => "Error";
    
    [JsonPropertyName("error")]
    public required string Error { get; init; }
}