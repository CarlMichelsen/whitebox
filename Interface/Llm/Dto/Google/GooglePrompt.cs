﻿using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.Google;

public record GooglePrompt(
    [property: JsonIgnore]
    string Model,
    [property: JsonPropertyName("contents")]
    List<GoogleContent> Contents,
    [property: JsonPropertyName("safetySettings")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    List<GoogleSafetySettings>? SafetySettings = null);