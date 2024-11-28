namespace LLMIntegration.Generic.Dto.Response;

public record LlmUsage(
    int InputTokens,
    int OutputTokens);