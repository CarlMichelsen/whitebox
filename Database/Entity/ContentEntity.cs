using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Database.Entity.Util;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class ContentEntity
{
    public required ContentEntityId Id { get; init; }
    
    public required MessageEntityId MessageId { get; init; }
    
    public required MessageEntity Message { get; init; }
    
    public required ContentType Type { get; init; }
    
    [MaxLength(1024 * 100)]
    public required string Value { get; init; }
    
    [Range(1, int.MaxValue)]
    public required int SortOrder { get; init; }
    
    public static void OnModelCreating(EntityTypeBuilder<ContentEntity> entity)
    {
        entity
            .Property(e => e.Id)
            .RegisterTypedIdConversion(guid => new ContentEntityId(guid));
        
        entity
            .HasOne(e => e.Message)
            .WithMany(e => e.Content)
            .HasForeignKey(e => e.MessageId);
    }
}