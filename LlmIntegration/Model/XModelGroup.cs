using LLMIntegration.Util;

namespace LLMIntegration.Model;

public class XModelGroup
{
    public LlmModel Grok3 { get; } = new(
        Provider: LlmProvider.X,
        ModelName: "Grok 3",
        ModelDescription: "Grok version 3.",
        ModelIdentifier: "grok-3-beta");
    
    public LlmModel Grok3Mini { get; } = new(
        Provider: LlmProvider.X,
        ModelName: "Grok 3 Mini",
        ModelDescription: "Grok version 3 (mini).",
        ModelIdentifier: "grok-3-mini-beta");
}