﻿using System.Text.Json.Serialization;

namespace Interface.Dto.Llm.OpenAi.Response.Stream;

public record OpenAiChunk(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("object")] string Object,
    [property: JsonPropertyName("created")] long Created,
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("system_fingerprint")] string SystemFingerprint,
    [property: JsonPropertyName("choices")] List<OpenAiChunkChoice> Choices);