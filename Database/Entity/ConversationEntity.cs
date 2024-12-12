using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Database.Entity.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class ConversationEntity : ISoftDeletable
{
    private MessageEntity? lastAppendedMessage;
    
    public required ConversationEntityId Id { get; init; }
    
    [MaxLength(1024)]
    public required string? Summary { get; set; }
    
    [MaxLength(1024 * 100)]
    public required string? SystemMessage { get; set; }
    
    public required long CreatorId { get; init; }
    
    public required UserEntity Creator { get; init; }
    
    public required List<MessageEntity> Messages { get; init; }
    
    public required DateTime CreatedUtc { get; init; }
    
    public required DateTime LastAlteredUtc { get; set; }
    
    public required MessageEntityId? LastAppendedMessageId { get; set; }
    
    public required MessageEntity? LastAppendedMessage
    {
        get => this.lastAppendedMessage!;
        set
        {
            if (value != null && value.ConversationId != this.Id)
            {
                throw new InvalidOperationException("The LastAppendedMessage must belong to the same conversation.");
            }
            
            this.lastAppendedMessage = value;
            this.LastAppendedMessageId = value?.Id;
        }
    }
    
    public DateTime? DeletedAtUtc { get; set; }
    
    public static void OnModelCreating(EntityTypeBuilder<ConversationEntity> entity, ModelBuilder modelBuilder)
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
        
        entity.MakeSoftDeletable(modelBuilder);
    }
}