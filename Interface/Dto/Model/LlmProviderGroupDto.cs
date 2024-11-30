namespace Interface.Dto.Model;

public record LlmProviderGroupDto(
    string Provider,
    List<LlmModelDto> Models);