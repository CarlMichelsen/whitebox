using LLMIntegration.Anthropic.Dto.Model;
using LLMIntegration.Google.Dto.Model;
using LLMIntegration.OpenAi.Dto.Model;
using LLMIntegration.X.Dto.Model;

namespace LLMIntegration.Util;

/// <summary>
/// A static class that contains models for each LlmProvider.
/// </summary>
public static class LlmModels
{
    public static AnthropicModelGroup Anthropic { get; } = new();
    
    public static GoogleModelGroup Google { get; } = new();
    
    public static OpenAiModelGroup OpenAi { get; } = new();
    
    public static XModelGroup X { get; } = new();
}