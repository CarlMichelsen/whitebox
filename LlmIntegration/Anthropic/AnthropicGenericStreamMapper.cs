using Interface.Llm;
using Interface.Llm.Dto.Anthropic.Response;
using Interface.Llm.Dto.Anthropic.Response.Stream;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;
using LLMIntegration.Util;

namespace LLMIntegration.Anthropic;

public class AnthropicGenericStreamMapper : ILlmStreamMapper<BaseAnthropicEvent>
{
    private readonly StreamDataCollector streamDataCollector = new();
    
    public LlmStreamEvent MapStreamEvent(BaseAnthropicEvent anthropicEvent)
    {
        return anthropicEvent switch
        {
            AnthropicMessageStart streamEvent => this.MessageStart(streamEvent),
            AnthropicMessageDelta streamEvent => this.MessageDelta(streamEvent),
            AnthropicMessageStop streamEvent => MessageStop(streamEvent),
            AnthropicContentBlockStart streamEvent => ContentBlockStart(streamEvent),
            AnthropicContentBlockDelta streamEvent => ContentBlockDelta(streamEvent),
            AnthropicContentBlockStop streamEvent => ContentBlockStop(streamEvent),
            AnthropicPing streamEvent => Ping(streamEvent),
            AnthropicError streamEvent => Error(streamEvent),
            _ => new LlmStreamError { Error = "Unknown Anthropic Event" },
        };
    }

    public LlmStreamEvent ConcludeStream()
    {
        return this.streamDataCollector.ConcludeStream();
    }
    
    private static LlmStreamPing MessageStop(AnthropicMessageStop anthropicEvent)
    {
        return new LlmStreamPing();
    }
    
    private static LlmStreamPing ContentBlockStart(AnthropicContentBlockStart anthropicEvent)
    {
        return new LlmStreamPing();
    }
    
    private static LlmStreamEvent ContentBlockDelta(AnthropicContentBlockDelta anthropicEvent)
    {
        if (anthropicEvent.Delta.Type != "text_delta")
        {
            return new LlmStreamError { Error = "Only \"text_delta\" anthropic stream events are supported" };
        }
        
        return new LlmStreamContentDelta
        {
            Delta = new LlmPart(
                PartType.Text,
                anthropicEvent.Delta.Text),
        };
    }
    
    private static LlmStreamPing ContentBlockStop(AnthropicContentBlockStop anthropicEvent)
    {
        return new LlmStreamPing();
    }
    
    private static LlmStreamPing Ping(AnthropicPing anthropicEvent)
    {
        return new LlmStreamPing();
    }
    
    private static LlmStreamError Error(AnthropicError anthropicEvent)
    {
        return new LlmStreamError { Error = anthropicEvent.Error.Message };
    }
    
    private LlmStreamPing MessageStart(AnthropicMessageStart anthropicEvent)
    {
        this.streamDataCollector.IncrementInputTokens(anthropicEvent.Message.Usage.InputTokens);
        this.streamDataCollector.IncrementOutputTokens(anthropicEvent.Message.Usage.OutputTokens);
        this.streamDataCollector.SetModel(anthropicEvent.Message.Model);
        this.streamDataCollector.SetResponseId(anthropicEvent.Message.Model);
        this.streamDataCollector.SetRole(anthropicEvent.Message.Role == "user" ? LlmRole.User : LlmRole.Assistant);
        return new LlmStreamPing();
    }
    
    private LlmStreamPing MessageDelta(AnthropicMessageDelta anthropicEvent)
    {
        this.streamDataCollector.IncrementOutputTokens(anthropicEvent.Usage.OutputTokens);
        this.streamDataCollector.SetFinnishReason(anthropicEvent.Delta.StopReason);
        return new LlmStreamPing();
    }
}