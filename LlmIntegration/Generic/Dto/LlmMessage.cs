namespace LLMIntegration.Generic.Dto;

public record LlmMessage(
    LlmRole Role,
    List<LlmPart> Parts);