namespace LLMIntegration.Generic.Dto;

public record LlmContent(
    List<LlmMessage> Messages,
    string? SystemMessage = default);