namespace Interface.Llm.Dto.Generic;

public record LlmPrompt(
    LlmModel Model,
    LlmContent Content,
    int MaxTokens);