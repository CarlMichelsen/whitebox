using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.OpenAi.Dto.Response.Stream;
using LLMIntegration.Util;

namespace LLMIntegration.OpenAi;

public class OpenAiGenericStreamMapper : ILlmStreamMapper<OpenAiChunk>
{
    private readonly StreamDataCollector streamDataCollector = new();
    
    public LlmStreamEvent MapStreamEvent(OpenAiChunk streamEvent)
    {
        var choice = streamEvent.Choices.FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(streamEvent.Model))
        {
            this.streamDataCollector.SetModel(streamEvent.Model);
        }
        
        if (!string.IsNullOrWhiteSpace(streamEvent.Id))
        {
            this.streamDataCollector.SetResponseId(streamEvent.Id);
        }
        
        if (streamEvent.Usage is not null)
        {
            this.streamDataCollector.SetInputTokens(streamEvent.Usage.PromptTokens);
            this.streamDataCollector.IncrementOutputTokens(streamEvent.Usage.CompletionTokens);
        }

        if (choice is null)
        {
            return new LlmStreamPing();
        }
        
        if (!string.IsNullOrWhiteSpace(choice.FinishReason))
        {
            this.streamDataCollector.SetFinnishReason(choice.FinishReason);
        }

        if (choice.Delta.Content is not null)
        {
            return new LlmStreamContentDelta
            {
                Delta = new LlmPart(
                    Type: PartType.Text,
                    Content: choice.Delta.Content),
            };
        }

        return new LlmStreamPing();
    }

    public LlmStreamEvent ConcludeStream()
    {
        this.streamDataCollector.SetRole(LlmRole.Assistant);
        return this.streamDataCollector.ConcludeStream();
    }
}