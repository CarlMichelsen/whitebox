namespace Interface.Llm.Dto.Generic;

public record LlmMessage(
    LlmRole Role,
    List<LlmPart> Parts);