﻿namespace LLMIntegration.Anthropic.Dto.Response.Stream;

public class AnthropicMessageStop : BaseAnthropicEvent
{
    public override string Type => "message_stop";
}