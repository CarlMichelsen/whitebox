using LLMIntegration.Util;

namespace LLMIntegration.Generic.Dto;

public record LlmPrompt(
    LlmModel Model,
    LlmContent Content,
    int MaxTokens);