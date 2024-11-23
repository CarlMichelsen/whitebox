namespace Interface.Dto.Llm.Anthropic.Response.Stream;

public class AnthropicPing : BaseAnthropicEvent
{
    public override string Type => "ping";
}