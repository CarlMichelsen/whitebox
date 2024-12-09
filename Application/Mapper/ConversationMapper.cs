using Database.Entity;
using Database.Entity.Id;
using Domain.Conversation;
using Domain.Exception;
using Domain.User;
using Interface.Dto.Conversation;
using Interface.Dto.Model;
using LLMIntegration.Util;

namespace Application.Mapper;

public static class ConversationMapper
{
    public static string CacheKeyFactory(AuthenticatedUser user, ConversationEntityId conversationEntityId)
    {
        return $"conversation_{user.Id}_{conversationEntityId.Value}";
    }
    
    public static ConversationDto Map(ConversationEntity conversationEntity, AuthenticatedUser creator)
    {
        return new ConversationDto(
            Id: conversationEntity.Id.Value,
            Creator: creator,
            SystemMessage: conversationEntity.SystemMessage,
            Summary: conversationEntity.Summary,
            Sections: GetSections(conversationEntity),
            CreatedUtc: TimeMapper.GetUnixTimeSeconds(conversationEntity.CreatedUtc),
            LastAlteredUtc: TimeMapper.GetUnixTimeSeconds(conversationEntity.LastAlteredUtc));
    }
    
    public static MessageDto Map(MessageEntity messageEntity)
    {
        UsageDto? usage = null;
        LlmModelDto? model = null;

        if (messageEntity.Prompt?.Usage is not null)
        {
            if (LlmModels.TryGetModel(
                    messageEntity.Prompt.Usage.InitialModelIdentifier,
                    out var foundModel))
            {
                model = AvailableModelsMapper.Map(foundModel!);
                usage = new UsageDto(
                    Id: messageEntity.Prompt.Usage.Id.Value,
                    InputTokens: messageEntity.Prompt.Usage.InputTokens,
                    OutputTokens: messageEntity.Prompt.Usage.OutputTokens,
                    InitialModelIdentifier: messageEntity.Prompt.Usage.InitialModelIdentifier,
                    SpecificModelIdentifier: messageEntity.Prompt.Usage.SpecificModelIdentifier);
            }
        }
        
        return new MessageDto(
            Id: messageEntity.Id.Value,
            PreviousMessageId: messageEntity.PreviousMessageId?.Value,
            AiModel: model,
            Content: messageEntity.Content.Select(Map).ToList(),
            Usage: usage,
            CreatedUtc: TimeMapper.GetUnixTimeSeconds(messageEntity.CreatedUtc));
    }

    public static string Map(ContentType contentType)
    {
        return Enum.GetName(contentType)!.ToLower();
    }

    private static List<ConversationSectionDto> GetSections(ConversationEntity conversationEntity)
    {
        var rootMessages = conversationEntity.Messages
            .Where(m => m.PreviousMessageId is null)
            .ToList();

        // Root section.
        var current = new ConversationSection
        {
            SelectedMessageId = null,
            Messages = rootMessages.ToDictionary(m => m.Id, m => m),
        };
        var sectionList = new List<ConversationSection>();
        while (current is not null)
        {
            sectionList.Add(current);
            current = current.GetNextSection(conversationEntity);
        }
        
        sectionList.SelectMessages(conversationEntity);
        
        return sectionList
            .Select(Map)
            .ToList();
    }

    private static ConversationSectionDto Map(ConversationSection conversationSection)
    {
        return new ConversationSectionDto(
            SelectedMessageId: conversationSection.SelectedMessageId?.Value,
            Messages: conversationSection.Messages.ToDictionary(kv => kv.Key.Value, kv => Map(kv.Value)));
    }

    private static MessageContentDto Map(ContentEntity contentEntity)
    {
        return new MessageContentDto(
            Id: contentEntity.Id.Value,
            Type: Map(contentEntity.Type),
            Value: contentEntity.Value,
            SortOrder: contentEntity.SortOrder);
    }

    private static ConversationSection? GetNextSection(
        this ConversationSection current,
        ConversationEntity conversationEntity)
    {
        var messageIds = current.Messages.Keys.ToList();
        var nextSectionMessages = conversationEntity.Messages
            .Where(m => messageIds.Contains(m.PreviousMessageId!))
            .ToList();

        if (nextSectionMessages.Count == 0)
        {
            return null;
        }
        
        return new ConversationSection
        {
            SelectedMessageId = null,
            Messages = nextSectionMessages.ToDictionary(m => m.Id, m => m),
        };
    }
    
    private static void SelectMessages(
        this List<ConversationSection> sections,
        ConversationEntity conversationEntity)
    {
        var lastSelectedMessageId = conversationEntity.LastAppendedMessageId;
        if (lastSelectedMessageId is null)
        {
            throw new IncompleteConversationEntityException(
                "No last appended message id was found.");
        }
        
        var sectionId = sections
            .FindIndex(s => s.Messages.ContainsKey(lastSelectedMessageId));

        if (sectionId == -1)
        {
            throw new IncompleteConversationEntityException(
                "No last appended message was found in mapped sections.");
        }
        
        for (var i = sectionId; i >= 0; i--)
        {
            if (lastSelectedMessageId is null && i != 0)
            {
                throw new IncompleteConversationEntityException(
                    "Failed to completely construct message sections.");
            }
            
            var section = sections[i];
            section.SelectedMessageId = lastSelectedMessageId;
            lastSelectedMessageId = section.Messages[lastSelectedMessageId!].PreviousMessageId;
        }
    }
}