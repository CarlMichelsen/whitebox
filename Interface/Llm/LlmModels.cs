using Interface.Llm.Dto.Anthropic.Model;
using Interface.Llm.Dto.Google.Model;
using Interface.Llm.Dto.OpenAi.Model;
using Interface.Llm.Dto.X.Model;

namespace Interface.Llm;

/// <summary>
/// A static class that contains models for each LlmProvider.
/// </summary>
public static partial class LlmModels
{
    public static AnthropicModelGroup Anthropic { get; } = new();
    
    public static GoogleModelGroup Google { get; } = new();
    
    public static OpenAiModelGroup OpenAi { get; } = new();
    
    public static XModelGroup X { get; } = new();
}