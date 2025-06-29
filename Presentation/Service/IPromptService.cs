using Database.Entity;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response.Stream;

namespace Presentation.Service;

public interface IPromptService
{
    Task<PromptEntity> Prompt(LlmPrompt prompt);
    
    Task<PromptEntity> StreamPrompt(LlmPrompt prompt, Func<LlmStreamEvent, Task> handler);
}