namespace Interface.Llm.Dto.Anthropic.Response.Stream;

public class AnthropicMessageStop : BaseAnthropicEvent
{
    public override string Type => "message_stop";
}