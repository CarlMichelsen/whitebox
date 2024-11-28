namespace Interface.Llm.Dto.Generic;

public record LlmContent(
    List<LlmMessage> Messages,
    string? SystemMessage = default);