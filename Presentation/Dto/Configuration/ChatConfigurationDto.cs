using Presentation.Dto.Model;

namespace Presentation.Dto.Configuration;

public record ChatConfigurationDto(
    string Id,
    string? DefaultSystemMessage,
    LlmModelDto SelectedModel,
    int MaxTokens,
    List<LlmProviderGroupDto> AvailableProviders);