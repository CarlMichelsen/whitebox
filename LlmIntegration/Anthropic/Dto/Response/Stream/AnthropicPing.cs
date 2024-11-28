namespace LLMIntegration.Anthropic.Dto.Response.Stream;

public class AnthropicPing : BaseAnthropicEvent
{
    public override string Type => "ping";
}