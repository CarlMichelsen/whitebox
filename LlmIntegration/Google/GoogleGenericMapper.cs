using LLMIntegration.Generic.Dto;
using LLMIntegration.Generic.Dto.Response;
using LLMIntegration.Google.Dto;
using LLMIntegration.Google.Dto.Response;

namespace LLMIntegration.Google;

public static class GoogleGenericMapper
{
    private const string AssistantString = "model";
    private const string UserString = "user";
    
    private const string DefaultSystemMessage = "Please do your best to assist me.";
    private const string AssistantSystemMessageAcknowledgement = "I understand, I will do that.";
    
    public static GooglePrompt Map(LlmPrompt prompt)
    {
        // System messages are not supported I'm gas-lighting the model.
        List<GoogleContent> messages =
                [
                    new(
                        Role: UserString,
                        Parts: [new GooglePart(Text: prompt.Content.SystemMessage ?? DefaultSystemMessage)]),
                    new(
                            Role: AssistantString,
                            Parts: [new GooglePart(Text: AssistantSystemMessageAcknowledgement)]),
                    ..prompt.Content.Messages.Select(Map).ToList()
                ];
        
        return new GooglePrompt(
            Model: prompt.Model.ModelIdentifier,
            Contents: messages);
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