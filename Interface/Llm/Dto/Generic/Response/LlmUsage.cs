namespace Interface.Llm.Dto.Generic.Response;

public record LlmUsage(
    int InputTokens,
    int OutputTokens);