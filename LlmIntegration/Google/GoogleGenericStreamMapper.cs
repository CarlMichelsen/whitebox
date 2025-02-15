﻿using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response.Stream;
using LLMIntegration.Google.Dto.Response.Stream;
using LLMIntegration.Util;

namespace LLMIntegration.Google;

public class GoogleGenericStreamMapper : ILlmStreamMapper<GoogleStreamChunk>
{
    private readonly StreamDataCollector streamDataCollector = new();
    
    public LlmStreamEvent MapStreamEvent(GoogleStreamChunk streamEvent)
    {
        var candidate = streamEvent.Candidates.FirstOrDefault();
        
        if (streamEvent.UsageMetadata.PromptTokenCount != 0)
        {
            this.streamDataCollector.SetInputTokens(streamEvent.UsageMetadata.PromptTokenCount);
        }
        
        var outputTokens = streamEvent.UsageMetadata.CandidatesTokenCount;
        if (outputTokens is not null)
        {
            this.streamDataCollector.IncrementOutputTokens((int)outputTokens);
        }

        if (!string.IsNullOrWhiteSpace(streamEvent.ModelVersion))
        {
            this.streamDataCollector.SetModel(streamEvent.ModelVersion);
        }

        if (candidate == null)
        {
            return new LlmStreamPing();
        }

        if (!string.IsNullOrWhiteSpace(candidate.FinishReason))
        {
            this.streamDataCollector.SetFinnishReason(candidate.FinishReason);
        }
        
        var text = string.Join(string.Empty, candidate.Content.Parts.Select(p => p.Text));
        return new LlmStreamContentDelta
        {
            Delta = new LlmPart(
                Type: PartType.Text,
                Content: text),
        };
    }

    public LlmStreamEvent ConcludeStream()
    {
        this.streamDataCollector.SetRole(LlmRole.Assistant);
        this.streamDataCollector.SetResponseId(Guid.CreateVersion7().ToString());
        return this.streamDataCollector.ConcludeStream();
    }
}