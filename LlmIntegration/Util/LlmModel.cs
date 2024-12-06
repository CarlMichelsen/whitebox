﻿namespace LLMIntegration.Util;

public record LlmModel(
    LlmProvider Provider,
    string ModelName,
    string ModelDescription,
    string ModelIdentifier,
    int MaxCompletionTokens = 2048);