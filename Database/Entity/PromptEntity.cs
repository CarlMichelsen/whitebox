using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Database.Entity.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class PromptEntity
{
    public required PromptEntityId Id { get; init; }
    
    public required UsageEntityId? UsageId { get; set; }
    
    public required UsageEntity? Usage { get; set; }
    
    public required long UserId { get; init; }
    
    public required UserEntity User { get; init; }
    
    [MaxLength(1024 * 200)]
    public required string PromptJson { get; init; }
    
    public required DateTime PromptUtc { get; init; }
    
    public required bool Stream { get; init; }
    
    public static void OnModelCreating(EntityTypeBuilder<PromptEntity> entity)
    {
        entity
            .Property(e => e.Id)
            .RegisterTypedIdConversion(guid => new PromptEntityId(guid));

        entity
            .Property(e => e.PromptJson)
            .HasColumnType("jsonb");
        
        entity
            .HasOne(e => e.Usage)
            .WithOne()
            .HasForeignKey<PromptEntity>(e => e.UsageId);
        
        entity
            .HasOne(e => e.User)
            .WithMany(e => e.Prompts)
            .HasForeignKey(e => e.UserId);
    }
}