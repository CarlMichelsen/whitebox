using LLMIntegration.Generic.Dto.Response.Stream;

namespace LLMIntegration.Util;

public static class LlmStreamEventHandler
{
    public static async IAsyncEnumerable<LlmStreamEvent> CreateAsyncEnumerable<T>(
        IAsyncEnumerable<T> enumerable,
        ILlmStreamMapper<T> mapper)
    {
        await foreach (var streamEvent in enumerable)
        {
            var mappedStreamEvent = mapper.MapStreamEvent(streamEvent);
            
            yield return mappedStreamEvent;
            if (mappedStreamEvent is LlmStreamError)
            {
                yield break;
            }
        }
        
        yield return mapper.ConcludeStream();
    }
}