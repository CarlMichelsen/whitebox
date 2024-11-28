﻿using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google;

public record GooglePart(
    [property: JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Text = null,
    [property: JsonPropertyName("inline_data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? InlineData = null);