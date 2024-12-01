using Interface.Dto.Model;

namespace Interface.Dto.Configuration;

public record ChatConfigurationDto(
    string Id,
    string? DefaultSystemMessage,
    LlmModelDto SelectedModel,
    int MaxTokens,
    List<LlmProviderGroupDto> AvailableModels);