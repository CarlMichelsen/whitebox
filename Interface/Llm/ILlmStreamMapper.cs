using Interface.Llm.Dto.Generic.Response.Stream;

namespace Interface.Llm;

public interface ILlmStreamMapper<in T>
{
    LlmStreamEvent MapStreamEvent(T streamEvent);
    
    LlmStreamEvent ConcludeStream();
}