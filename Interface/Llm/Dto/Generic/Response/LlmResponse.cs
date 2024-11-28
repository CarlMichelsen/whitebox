namespace Interface.Llm.Dto.Generic.Response;

public record LlmResponse(
    string ResponseId,
    string ModelIdentifier,
    LlmRole Role,
    string StopReason,
    List<LlmPart> Parts,
    LlmUsage Usage);