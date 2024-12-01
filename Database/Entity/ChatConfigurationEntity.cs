using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Microsoft.EntityFrameworkCore;

namespace Database.Entity;

public class ChatConfigurationEntity
{
    public required UserChatConfigurationId Id { get; init; }
    
    public required long UserId { get; init; }
    
    public UserEntity? User { get; set; }
    
    [MaxLength(1024 * 100)]
    public required string? DefaultSystemMessage { get; set; }
    
    [MaxLength(256)]
    public required string SelectedModelIdentifier { get; set; }
    
    [Range(1, int.MaxValue)]
    public required int MaxTokens { get; init; }

    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatConfigurationEntity>(entity =>
        {
            entity
                .Property(e => e.Id)
                .RegisterTypedIdConversion(guid => new UserChatConfigurationId(guid));
            
            entity
                .HasOne(e => e.User)
                .WithOne(e => e.ChatConfiguration)
                .HasForeignKey<ChatConfigurationEntity>(e => e.UserId);
        });
    }
}