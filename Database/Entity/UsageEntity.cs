using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Database.Entity.Util;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class UsageEntity
{
    public required UsageEntityId Id { get; init; }
    
    [MaxLength(128)]
    public required string Provider { get; init; }

    [MaxLength(128)]
    public required string InitialModelIdentifier { get; init; }
    
    [MaxLength(128)]
    public required string SpecificModelIdentifier { get; init; }
    
    public required PromptEntityId PromptId { get; init; }
    
    public required PromptEntity Prompt { get; init; }
    
    [MaxLength(1024 * 100)]
    public required string Completion { get; init; }
    
    public required int InputTokens { get; init; }
    
    public required int OutputTokens { get; init; }
    
    public required DateTime CompleteUtc { get; init; }
    
    public static void OnModelCreating(EntityTypeBuilder<UsageEntity> entity)
    {
        entity
            .Property(e => e.Id)
            .RegisterTypedIdConversion(guid => new UsageEntityId(guid));
        
        entity
            .HasOne(e => e.Prompt)
            .WithOne()
            .HasForeignKey<UsageEntity>(e => e.PromptId);
    }
}