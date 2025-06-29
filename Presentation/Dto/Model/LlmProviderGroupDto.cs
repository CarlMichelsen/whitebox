using System.Text.Json.Serialization;

namespace Presentation.Dto.Model;

public record LlmProviderGroupDto(
    [property: JsonPropertyName("provider")] string Provider,
    [property: JsonPropertyName("models")] List<LlmModelDto> Models);