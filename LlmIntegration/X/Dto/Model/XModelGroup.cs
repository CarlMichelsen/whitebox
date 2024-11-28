using LLMIntegration.Util;

namespace LLMIntegration.X.Dto.Model;

public class XModelGroup
{
    public LlmModel GrokBeta { get; } = new(
        Provider: LlmProvider.X,
        ModelName: "Grok Beta",
        ModelDescription: "Comparable performance to Grok 2 but with improved efficiency, speed and capabilities.",
        ModelIdentifier: "grok-beta");
}