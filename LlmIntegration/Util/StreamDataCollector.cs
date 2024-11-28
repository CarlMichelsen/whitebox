using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Generic.Response.Stream;

namespace LLMIntegration.Util;

public class StreamDataCollector
{
    private int inputTokens = 0;
    private int outputTokens = 0;
    private string? responseId = default;
    private string? model = default;
    private string? finnishReason = default;
    private LlmRole? role = default;

    public void SetInputTokens(int amount)
    {
        this.inputTokens = amount;
    }
    
    public void IncrementInputTokens(int amount)
    {
        this.inputTokens += amount;
    }
    
    public void IncrementOutputTokens(int amount)
    {
        this.outputTokens += amount;
    }
    
    public void SetResponseId(string newResponseId)
    {
        this.responseId = newResponseId;
    }
    
    public void SetModel(string newModel)
    {
        this.model = newModel;
    }
    
    public void SetFinnishReason(string newFinnishReason)
    {
        this.finnishReason = newFinnishReason;
    }
    
    public void SetRole(LlmRole newRole)
    {
        this.role = newRole;
    }
    
    public LlmStreamEvent ConcludeStream()
    {
        if (this.inputTokens == 0)
        {
            return new LlmStreamError { Error = "Zero input tokens." };
        }
        
        if (this.outputTokens == 0)
        {
            return new LlmStreamError { Error = "Zero output tokens." };
        }
        
        if (string.IsNullOrWhiteSpace(this.model))
        {
            return new LlmStreamError { Error = "No model." };
        }
        
        if (string.IsNullOrWhiteSpace(this.responseId))
        {
            return new LlmStreamError { Error = "No responseId." };
        }
        
        if (string.IsNullOrWhiteSpace(this.finnishReason))
        {
            return new LlmStreamError { Error = "No finnish reason." };
        }
        
        if (this.role is null)
        {
            return new LlmStreamError { Error = "No role defined." };
        }

        return new LlmStreamConclusion
        {
            ResponseId = this.responseId,
            ModelIdentifier = this.model,
            StopReason = this.finnishReason,
            Usage = new LlmUsage(
                InputTokens: this.inputTokens,
                OutputTokens: this.outputTokens),
        };
    }
}