﻿namespace Interface.Client;

public interface ILlmClient<in TPrompt, TPromptResponse, out TStreamEvent>
{
    Task<TPromptResponse> Prompt(TPrompt prompt);
    
    IAsyncEnumerable<TStreamEvent> StreamPrompt(TPrompt prompt);
}