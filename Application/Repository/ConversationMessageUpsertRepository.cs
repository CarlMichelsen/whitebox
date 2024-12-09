using Application.Mapper;
using Database;
using Database.Entity;
using Database.Entity.Id;
using Domain.Conversation.Action;
using Domain.Exception;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class ConversationMessageUpsertRepository(
    ApplicationContext applicationContext,
    IFullConversationReaderRepository fullConversationReaderRepository) : IConversationMessageUpsertRepository
{
    public async Task<ConversationEntity> AppendUserMessage(
        long userId,
        ChatConfigurationEntity chatConfiguration,
        AppendConversation appendConversation)
    {
        ConversationEntity? conversation;
        if (appendConversation.ReplyTo is null)
        {
            var foundUser = await applicationContext.User.FindAsync(userId);
            if (foundUser is null)
            {
                throw new ItemNotFoundException("Did not find user to make reply");
            }
            
            conversation = new ConversationEntity
            {
                Id = new ConversationEntityId(Guid.CreateVersion7()),
                SystemMessage = chatConfiguration.DefaultSystemMessage ?? string.Empty,
                Summary = null,
                Creator = foundUser,
                CreatorId = foundUser.Id,
                Messages = [],
                CreatedUtc = DateTime.UtcNow,
                LastAlteredUtc = DateTime.UtcNow,
                LastAppendedMessageId = default,
                LastAppendedMessage = default,
            };
            
            await applicationContext.Conversation.AddAsync(conversation);
            await applicationContext.SaveChangesAsync();

            var message = MessageCreator.CreateMessageFromText(
                conversation,
                null,
                appendConversation.Text);
            conversation.Messages.Add(message);
            conversation.LastAppendedMessageId = message.Id;
            conversation.LastAppendedMessage = message;
        }
        else
        {
            conversation = await fullConversationReaderRepository
                .GetConversation(userId, appendConversation.ReplyTo.ConversationId);

            if (conversation is null)
            {
                throw new ItemNotFoundException("Did not find conversation to reply to");
            }

            MessageEntity? replyToMessage = null;
            if (appendConversation.ReplyTo.ReplyToMessageId is not null)
            {
                replyToMessage = conversation.Messages
                    .FirstOrDefault(m => m.Id == appendConversation.ReplyTo.ReplyToMessageId);
                
                if (replyToMessage is null)
                {
                    throw new ItemNotFoundException("Did not find the message to reply to");
                }
            }
            
            var message = MessageCreator.CreateMessageFromText(
                conversation,
                null,
                appendConversation.Text,
                replyToMessage);
            conversation.Messages.Add(message);
            conversation.LastAppendedMessageId = message.Id;
            conversation.LastAppendedMessage = message;
        }

        return conversation;
    }

    public void ReplyToLatestMessage(
        ConversationEntity conversation,
        MessageEntityId promptMessageEntityId,
        ContentEntityId assistantMessageContentId,
        ContentType contentType,
        int sortOrder,
        PromptEntity promptEntity)
    {
        if (promptEntity.Usage is null)
        {
            throw new IncompletePromptEntityException("Prompt is incomplete");
        }
        
        var message = new MessageEntity
        {
            Id = promptMessageEntityId,
            ConversationId = conversation.Id,
            Conversation = conversation,
            PromptId = promptEntity.Id,
            Prompt = promptEntity,
            PreviousMessageId = conversation.LastAppendedMessageId,
            PreviousMessage = conversation.LastAppendedMessage,
            NextMessages = [],
            Content = [],
            CreatedUtc = DateTime.UtcNow,
        };

        var content = new ContentEntity
        {
            Id = assistantMessageContentId,
            MessageId = message.Id,
            Message = message,
            Type = contentType,
            Value = promptEntity.Usage.Completion,
            SortOrder = sortOrder,
        };
        
        message.Content.Add(content);
        
        conversation.Messages.Add(message);
        conversation.LastAppendedMessage?.NextMessages.Add(message);
        
        conversation.LastAppendedMessageId = message.Id;
        conversation.LastAppendedMessage = message;
        conversation.LastAlteredUtc = DateTime.UtcNow;
    }
}