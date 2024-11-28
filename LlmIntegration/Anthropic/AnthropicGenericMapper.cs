using Interface.Llm.Dto.Anthropic;
using Interface.Llm.Dto.Anthropic.Response;
using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;

namespace LLMIntegration.Anthropic;

public static class AnthropicGenericMapper
{
    public static AnthropicPrompt Map(LlmPrompt prompt)
    {
        return new AnthropicPrompt(
            Model: prompt.Model.ModelIdentifier,
            System: prompt.Content.SystemMessage,
            MaxTokens: prompt.MaxTokens,
            Messages: prompt.Content.Messages.Select(Map).ToList());
    }
    
    public static LlmResponse Map(AnthropicResponse response)
    {
        return new LlmResponse(
            ResponseId: response.Id,
            ModelIdentifier: response.Model,
            Role: response.Role == "assistant" ? LlmRole.Assistant : LlmRole.User,
            StopReason: response.StopReason,
            Parts: response.Content.Select(Map).ToList(),
            Usage: Map(response.Usage));
    }
    
    private static LlmPart Map(AnthropicContent content)
    {
        return new LlmPart(
            Type: PartType.Text,
            Content: content.Text);
    }

    private static LlmUsage Map(AnthropicUsage usage)
    {
        return new LlmUsage(
            InputTokens: usage.InputTokens,
            OutputTokens: usage.OutputTokens);
    }
    
    private static AnthropicMessage Map(LlmMessage message)
    {
        return new AnthropicMessage(
            Role: message.Role == LlmRole.Assistant ? "assistant" : "user",
            Content: message.Parts.Select(Map).ToList());
    }

    private static AnthropicContent Map(LlmPart part)
    {
        var type = part.Type == PartType.Text
            ? "text"
            : throw new NotSupportedException("Only text parts are supported for anthropic mappings.");
        return new AnthropicContent(
            Type: type,
            Text: part.Content);
    }
}