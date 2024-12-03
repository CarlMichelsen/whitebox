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
    ApplicationContext applicationContext) : IConversationMessageUpsertRepository
{
    public async Task<(ConversationEntity Conversation, MessageEntity Message)> AppendUserMessage(
        long userId,
        AppendConversation appendConversation)
    {
        ConversationEntity? conversation;
        MessageEntity message;
        if (appendConversation.ReplyTo is null)
        {
            var foundUser = await applicationContext.User.FindAsync(userId);
            if (foundUser is null)
            {
                throw new ItemNotFoundException("Did not find user to make reply");
            }
            
            conversation = new ConversationEntity
            {
                Id = new ConversationEntityId(Guid.NewGuid()),
                Creator = foundUser,
                CreatorId = foundUser.Id,
                Messages = [],
                CreatedUtc = DateTime.UtcNow,
                LastAppendedUtc = DateTime.UtcNow,
                LastAppendedMessageId = default,
                LastAppendedMessage = default,
            };

            message = MessageCreator.CreateMessageFromText(
                conversation,
                null,
                appendConversation.Text);
            conversation.Messages.Add(message);
            conversation.LastAppendedMessageId = message.Id;
            conversation.LastAppendedMessage = message;

            applicationContext.Conversation.Add(conversation);
        }
        else
        {
            conversation = await applicationContext.Conversation
                .Where(c => c.CreatorId == userId && c.Id == appendConversation.ReplyTo!.ConversationId)
                .Include(c => c.Messages)
                    .ThenInclude(messageEntity => messageEntity.Id)
                .Include(c => c.LastAppendedMessage)
                .Include(c => c.Creator)
                .FirstOrDefaultAsync();

            if (conversation is null)
            {
                throw new ItemNotFoundException("Did not find conversation to reply to");
            }

            var replyToMessage = conversation.Messages
                .FirstOrDefault(m => m.Id == appendConversation.ReplyTo.ReplyToMessageId);

            if (replyToMessage is null)
            {
                throw new ItemNotFoundException("Did not find message to reply to");
            }
            
            message = MessageCreator.CreateMessageFromText(
                conversation,
                null,
                appendConversation.Text,
                replyToMessage);
            conversation.Messages.Add(message);
            conversation.LastAppendedMessageId = message.Id;
            conversation.LastAppendedMessage = message;
        }

        return (Conversation: conversation, Message: message);
    }
}