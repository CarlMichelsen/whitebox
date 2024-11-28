using System.Text.Json.Serialization;

namespace LLMIntegration.Google.Dto;

public record GooglePart(
    [property: JsonPropertyName("text"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Text = null,
    [property: JsonPropertyName("inline_data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? InlineData = null);