namespace Interface.Dto.Model;

public record LlmModelDto(
    string Provider,
    string ModelName,
    string ModelDescription,
    string ModelIdentifier);