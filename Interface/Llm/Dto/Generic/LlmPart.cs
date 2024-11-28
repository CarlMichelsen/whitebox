namespace Interface.Llm.Dto.Generic;

public record LlmPart(
    PartType Type,
    string Content);