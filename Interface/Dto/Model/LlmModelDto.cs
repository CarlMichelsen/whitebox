﻿using System.Text.Json.Serialization;

namespace Interface.Dto.Model;

public record LlmModelDto(
    [property: JsonPropertyName("provider")] string Provider,
    [property: JsonPropertyName("modelName")] string ModelName,
    [property: JsonPropertyName("modelDescription")] string ModelDescription,
    [property: JsonPropertyName("modelIdentifier")] string ModelIdentifier);