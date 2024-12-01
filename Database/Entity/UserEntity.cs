using System.ComponentModel.DataAnnotations;
using Database.Entity.Id;
using Microsoft.EntityFrameworkCore;

namespace Database.Entity;

public class UserEntity
{
    public required long Id { get; init; }
    
    [MaxLength(512)]
    public required string AuthenticationMethod { get; init; }
    
    [MaxLength(128)]
    public required string AuthenticationId { get; init; }
    
    public required UserChatConfigurationId ChatConfigurationId { get; init; }
    
    public ChatConfigurationEntity? ChatConfiguration { get; set; }
    
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity
                .HasOne(e => e.ChatConfiguration)
                .WithOne(e => e.User)
                .HasForeignKey<UserEntity>(e => e.ChatConfigurationId);
        });
    }
}