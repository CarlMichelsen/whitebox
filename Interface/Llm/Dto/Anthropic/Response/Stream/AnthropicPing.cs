namespace Interface.Llm.Dto.Anthropic.Response.Stream;

public class AnthropicPing : BaseAnthropicEvent
{
    public override string Type => "ping";
}