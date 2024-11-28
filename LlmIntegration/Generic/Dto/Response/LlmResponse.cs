namespace LLMIntegration.Generic.Dto.Response;

public record LlmResponse(
    string ResponseId,
    string ModelIdentifier,
    LlmRole Role,
    string StopReason,
    List<LlmPart> Parts,
    LlmUsage Usage);