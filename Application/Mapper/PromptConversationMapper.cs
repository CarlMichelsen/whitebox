using Database.Entity;
using Domain.Exception;
using LLMIntegration;
using LLMIntegration.Generic.Dto;

namespace Application.Mapper;

public static class PromptConversationMapper
{
    public static LlmPrompt CreatePromptFromLatestUserMessage(
        ConversationEntity conversation,
        string modelIdentifier,
        int maxTokens,
        bool useLatest = false)
    {
        var branchMessage = conversation.LastAppendedMessage!.PromptId is not null
            ? conversation.Messages.FirstOrDefault(m => m.Id == conversation.LastAppendedMessage.PreviousMessageId!)
            : conversation.LastAppendedMessage;

        if (useLatest)
        {
            branchMessage = conversation.LastAppendedMessage;
        }

        if (branchMessage is null)
        {
            throw new IncompleteConversationEntityException(
                "No user-message found to reply to when mapping llm-prompt");
        }
        
        var messageChain = CreateMessageLinkedList(conversation, branchMessage);

        return CreateLlmPrompt(messageChain, modelIdentifier, maxTokens, conversation.SystemMessage);
    }

    private static LlmPrompt CreateLlmPrompt(
        LinkedList<MessageEntity> messages,
        string modelIdentifier,
        int maxTokens,
        string? conversationSystemMessage)
    {
        if (!LlmModels.TryGetModel(modelIdentifier, out var model))
        {
            throw new PromptMapException("ChatConfiguration model does not exist");
        }
        
        var content = new LlmContent(
            Messages: messages.Select(Map).ToList(),
            SystemMessage: conversationSystemMessage);

        var clampedMaxTokens = maxTokens >= model!.MaxCompletionTokens
            ? model.MaxCompletionTokens - 1
            : maxTokens;
        return new LlmPrompt(
            Model: model!,
            Content: content,
            MaxTokens: clampedMaxTokens);
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