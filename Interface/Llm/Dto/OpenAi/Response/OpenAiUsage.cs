﻿using System.Text.Json.Serialization;

namespace Interface.Llm.Dto.OpenAi.Response;

public record OpenAiUsage(
    [property: JsonPropertyName("prompt_tokens")] int PromptTokens,
    [property: JsonPropertyName("completion_tokens")] int CompletionTokens,
    [property: JsonPropertyName("total_tokens")] int TotalTokens);