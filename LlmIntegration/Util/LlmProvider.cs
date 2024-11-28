namespace LLMIntegration.Util;

public enum LlmProvider
{
    /// <summary>
    /// Anthropic Llm Provider.
    /// Supports Anthropic models.
    /// </summary>
    Anthropic,
    
    /// <summary>
    /// Google Llm provider.
    /// Supports Google models.
    /// </summary>
    Google,
    
    /// <summary>
    /// OpenAi Llm provider.
    /// Supports OpenAi models.
    /// </summary>
    OpenAi,
    
    /// <summary>
    /// X Llm provider.
    /// Supports X models.
    /// </summary>
    X,
}