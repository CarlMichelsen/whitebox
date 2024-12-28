using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Database.Entity.Util;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class RedirectEntity
{
    public required RedirectEntityId Id { get; init; }
    
    public required long? UserId { get; init; }
    
    public required UserEntity? User { get; init; }
    
    public required SourceId SourceId { get; init; }
    
    [MaxLength(80001)]
    public required string Url { get; init; }
    
    public required DateTime RedirectedAtUtc { get; init; }
    
    public static void OnModelCreating(EntityTypeBuilder<RedirectEntity> entity)
    {
        entity
            .Property(e => e.Id)
            .RegisterTypedIdConversion(guid => new RedirectEntityId(guid));
        
        entity
            .Property(e => e.SourceId)
            .RegisterTypedIdConversion(guid => new SourceId(guid));
        
        entity
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId);
    }
}