using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.OpenAi;
using Interface.Llm.Dto.OpenAi.Response;

namespace LLMIntegration.OpenAi;

public static class OpenAiGenericMapper
{
    public static OpenAiPrompt Map(LlmPrompt prompt)
    {
        var messages = new List<OpenAiMessage>();
        
        if (!string.IsNullOrWhiteSpace(prompt.Content.SystemMessage))
        {
            messages.Add(new OpenAiMessage(
                Role: "system",
                Content: [
                    new OpenAiContent(
                        Type: "text",
                        Text: prompt.Content.SystemMessage)
                ]));
        }
        
        messages.AddRange(prompt.Content.Messages.Select(Map));
        
        return new OpenAiPrompt(
            Model: prompt.Model.ModelIdentifier,
            Messages: messages,
            MaxTokens: prompt.MaxTokens);
    }

    public static LlmResponse Map(OpenAiResponse openAiResponse)
    {
        var choice = openAiResponse.Choices.First();
        
        return new LlmResponse(
            ResponseId: openAiResponse.Id,
            ModelIdentifier: openAiResponse.Model,
            Role: choice.ResponseMessage.Role == "user" ? LlmRole.User : LlmRole.Assistant,
            StopReason: choice.FinishReason,
            Parts: [
                new LlmPart(
                    Type: PartType.Text,
                    Content: choice.ResponseMessage.Content),
            ],
            Usage: Map(openAiResponse.Usage));
    }

    private static LlmUsage Map(OpenAiUsage usage)
    {
        return new LlmUsage(
            InputTokens: usage.PromptTokens,
            OutputTokens: usage.CompletionTokens);
    }

    private static OpenAiMessage Map(LlmMessage llmMessage)
    {
        return new OpenAiMessage(
            Role: llmMessage.Role == LlmRole.Assistant ? "assistant" : "user",
            Content: llmMessage.Parts.Select(Map).ToList());
    }
    
    private static OpenAiContent Map(LlmPart part)
    {
        var type = part.Type == PartType.Text
            ? "text"
            : throw new NotSupportedException("Only text parts are supported for openai mappings.");
       
        return new OpenAiContent(
            Type: type,
            Text: part.Content);
    }
}