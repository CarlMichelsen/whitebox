using Database.Entity;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Util;

namespace Application.Mapper;

public static class SummaryPromptMapper
{
    private const string SummaryRequest = "Create a very short (6 words or less) summary of the conversation so far.";
    
    private const string SystemMessage =
        $"Capture the essence of the conversation and when prompted with \"{SummaryRequest}\" respond with a short symmary.";
    
    public static LlmPrompt SummaryPrompt(ConversationEntity conversation)
    {
        var convPrompt = PromptConversationMapper.CreatePromptFromLatestUserMessage(
            conversation,
            LlmModels.OpenAi.Gpt4OMini.ModelIdentifier,
            LlmModels.OpenAi.Gpt4OMini.MaxCompletionTokens,
            true);

        var messages = convPrompt.Content.Messages;
        messages.Add(new LlmMessage(
            Role: LlmRole.User,
            Parts: [new LlmPart(PartType.Text, SummaryRequest)]));
        
        return convPrompt with {
            Content = new LlmContent(
            SystemMessage: SystemMessage,
            Messages: messages),
        };
    }
}