namespace Interface.Llm;

public record LlmModel(
    LlmProvider Provider,
    string ModelName,
    string ModelDescription,
    string ModelIdentifier);