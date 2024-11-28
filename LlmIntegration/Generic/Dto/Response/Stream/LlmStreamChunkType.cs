namespace LLMIntegration.Generic.Dto.Response.Stream;

public enum LlmStreamChunkType
{
    /// <summary>
    /// Stream ping event.
    /// Used to keep the connection alive.
    /// </summary>
    Ping,
    
    /// <summary>
    /// Message delta event.
    /// </summary>
    ContentDelta,
    
    /// <summary>
    /// Error event.
    /// </summary>
    Error,
    
    /// <summary>
    /// Final stream conclusion event.
    /// </summary>
    Conclusion,
}