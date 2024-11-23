namespace Interface.Dto.Llm.Anthropic.Response.Stream;

public class AnthropicMessageStop : BaseAnthropicEvent
{
    public override string Type => "message_stop";
}