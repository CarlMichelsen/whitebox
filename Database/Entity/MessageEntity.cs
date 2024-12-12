using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Database.Entity.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class MessageEntity : ISoftDeletable
{
    public required MessageEntityId Id { get; init; }
    
    public required ConversationEntityId ConversationId { get; init; }
    
    public required ConversationEntity Conversation { get; init; }
    
    public required PromptEntityId? PromptId { get; init; }
    
    public required PromptEntity? Prompt { get; init; }
    
    public required MessageEntityId? PreviousMessageId { get; init; }
    
    public required MessageEntity? PreviousMessage { get; init; }
    
    public required List<MessageEntity> NextMessages { get; init; }
    
    public required List<ContentEntity> Content { get; init; }
    
    public required DateTime CreatedUtc { get; init; }
    
    public DateTime? DeletedAtUtc { get; set; }
    
    public static void OnModelCreating(EntityTypeBuilder<MessageEntity> entity, ModelBuilder modelBuilder)
    {
        entity
            .Property(e => e.Id)
            .RegisterTypedIdConversion(guid => new MessageEntityId(guid));
        
        entity
            .HasOne(e => e.Conversation)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => e.ConversationId);
        
        entity
            .HasOne(e => e.PreviousMessage)
            .WithMany(e => e.NextMessages)
            .HasForeignKey(e => e.PreviousMessageId);
        
        entity
            .HasOne(e => e.Prompt)
            .WithMany()
            .HasForeignKey(e => e.PromptId);

        entity.MakeSoftDeletable(modelBuilder);
    }
}