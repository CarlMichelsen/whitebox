using LLMIntegration.Generic.Dto.Response.Stream;

namespace LLMIntegration.Util;

public interface ILlmStreamMapper<in T>
{
    LlmStreamEvent MapStreamEvent(T streamEvent);
    
    LlmStreamEvent ConcludeStream();
}