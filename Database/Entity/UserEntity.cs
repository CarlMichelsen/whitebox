using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity;

public class UserEntity
{
    public required long Id { get; init; }
    
    [MaxLength(512)]
    public required string AuthenticationMethod { get; init; }
    
    [MaxLength(128)]
    public required string AuthenticationId { get; init; }
    
    public required List<PromptEntity> Prompts { get; init; }
    
    public required List<ConversationEntity> Conversations { get; init; }
    
    public required UserChatConfigurationEntityId ChatConfigurationId { get; init; }
    
    public ChatConfigurationEntity? ChatConfiguration { get; init; }
    
    public required DateTime FirstLoginUtc { get; init; }
    
    public static void OnModelCreating(EntityTypeBuilder<UserEntity> entity)
    {
        entity
            .HasOne(e => e.ChatConfiguration)
            .WithOne(e => e.User)
            .HasForeignKey<UserEntity>(e => e.ChatConfigurationId);
        
        entity
            .HasMany(e => e.Conversations)
            .WithOne(e => e.Creator)
            .HasForeignKey(e => e.CreatorId);
    }
}