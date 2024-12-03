using Database.Entity.Id;
using Database.Entity.Util;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class ConversationEntity
{
    private MessageEntity? lastAppendedMessage;
    
    public required ConversationEntityId Id { get; init; }
    
    public required long CreatorId { get; init; }
    
    public required UserEntity Creator { get; init; }
    
    public required List<MessageEntity> Messages { get; init; }
    
    public required DateTime CreatedUtc { get; init; }
    
    public required DateTime LastAppendedUtc { get; set; }
    
    public required MessageEntityId LastAppendedMessageId { get; set; }
    
    public required MessageEntity LastAppendedMessage
    {
        get => this.lastAppendedMessage!;
        set
        {
            if (value != null && value.ConversationId != this.Id)
            {
                throw new InvalidOperationException("The LastAppendedMessage must belong to the same conversation.");
            }
            
            this.lastAppendedMessage = value;
            this.LastAppendedMessageId = value!.Id;
        }
    }
    
    public static void OnModelCreating(EntityTypeBuilder<ConversationEntity> entity)
    {
        entity
            .Property(e => e.Id)
            .RegisterTypedIdConversion(guid => new ConversationEntityId(guid));
        
        entity
            .HasMany(e => e.Messages)
            .WithOne(e => e.Conversation)
            .HasForeignKey(e => e.ConversationId);
        
        entity
            .HasOne(e => e.Creator)
            .WithMany(e => e.Conversations)
            .HasForeignKey(e => e.CreatorId);
        
        entity
            .HasOne(e => e.LastAppendedMessage)
            .WithMany()
            .HasForeignKey(e => e.LastAppendedMessageId);
    }
}