using Database.Entity;
using Domain.Exception;
using LLMIntegration.Generic.Dto;
using LLMIntegration.Util;

namespace Application.Mapper;

public static class PromptConversationMapper
{
    public static LlmPrompt CreatePromptFromLatestUserMessage(
        ConversationEntity conversation,
        ChatConfigurationEntity chatConfiguration)
    {
        var latestUserMessage = conversation.LastAppendedMessage!.PromptId is not null
            ? conversation.Messages.FirstOrDefault(m => m.Id == conversation.LastAppendedMessage.PreviousMessageId!)
            : conversation.LastAppendedMessage;

        if (latestUserMessage is null)
        {
            throw new IncompleteConversationEntityException(
                "No user-message found to reply to when mapping llm-prompt");
        }
        
        var messageChain = CreateMessageLinkedList(conversation, latestUserMessage);

        return CreateLlmPrompt(messageChain, chatConfiguration, conversation.SystemMessage);
    }

    private static LlmPrompt CreateLlmPrompt(
        LinkedList<MessageEntity> messages,
        ChatConfigurationEntity chatConfiguration,
        string? conversationSystemMessage)
    {
        if (!LlmModels.TryGetModel(chatConfiguration.SelectedModelIdentifier, out var model))
        {
            throw new PromptMapException("ChatConfiguration model does not exist");
        }
        
        var content = new LlmContent(
            Messages: messages.Select(Map).ToList(),
            SystemMessage: conversationSystemMessage);

        var maxTokens = chatConfiguration.MaxTokens >= model!.MaxCompletionTokens
            ? model.MaxCompletionTokens - 1
            : chatConfiguration.MaxTokens;
        return new LlmPrompt(
            Model: model!,
            Content: content,
            MaxTokens: maxTokens);
    }

    private static LlmMessage Map(MessageEntity message)
    {
        return new LlmMessage(
            Role: message.PromptId is null ? LlmRole.User : LlmRole.Assistant,
            Parts: message.Content.Select(Map).ToList());
    }
    
    private static LlmPart Map(ContentEntity content)
    {
        if (content.Type != ContentType.Text)
        {
            throw new PromptMapException("Only Text Content is supported");
        }
        
        return new LlmPart(
            Type: PartType.Text,
            Content: content.Value);
    }

    private static LinkedList<MessageEntity> CreateMessageLinkedList(ConversationEntity conversation, MessageEntity from)
    {
        var messageChain = new LinkedList<MessageEntity>();
        var current = messageChain.AddLast(from);

        var next = from;
        while (true)
        {
            next = conversation.Messages.FirstOrDefault(m => m.Id == next.PreviousMessageId!);
            if (next is null)
            {
                break;
            }
            
            current = messageChain.AddBefore(current, next);
        }
        
        return messageChain;
    }
}