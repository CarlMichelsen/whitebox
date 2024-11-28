using Interface.Llm.Dto.Generic;
using Interface.Llm.Dto.Generic.Response;
using Interface.Llm.Dto.Google;
using Interface.Llm.Dto.Google.Response;

namespace LLMIntegration.Google;

public static class GoogleGenericMapper
{
    private const string AssistantString = "model";
    private const string UserString = "user";
    
    public static GooglePrompt Map(LlmPrompt prompt)
    {
        // System messages are not supported :(
        return new GooglePrompt(
            Model: prompt.Model.ModelIdentifier,
            Contents: prompt.Content.Messages.Select(Map).ToList());
    }
    
    public static LlmResponse Map(GoogleResponse response, string responseId)
    {
        var candidate = response.Candidates.First();
        var parts = candidate.Content.Parts.Select(Map).ToList();
        
        return new LlmResponse(
            ResponseId: responseId,
            ModelIdentifier: response.ModelVersion,
            Role: candidate.Content.Role == AssistantString ? LlmRole.Assistant : LlmRole.User,
            StopReason: candidate.FinishReason,
            Parts: parts,
            Usage: Map(response.UsageMetadata));
    }

    private static LlmPart Map(GooglePart part)
    {
        return new LlmPart(
            Type: PartType.Text,
            Content: part.Text!);
    }

    private static LlmUsage Map(GoogleUsage usage)
    {
        return new LlmUsage(
            InputTokens: usage.PromptTokenCount,
            OutputTokens: usage.CandidatesTokenCount ?? 0);
    }

    private static GoogleContent Map(LlmMessage message)
    {
        return new GoogleContent(
            Role: message.Role == LlmRole.Assistant ? AssistantString : UserString,
            Parts: message.Parts.Select(Map).ToList());
    }

    private static GooglePart Map(LlmPart part)
    {
        if (part.Type != PartType.Text)
        {
            throw new NotSupportedException("Only text parts are supported for google mappings.");
        }
        
        return new GooglePart(Text: part.Content);
    }
}