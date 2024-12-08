using Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Database;

/// <summary>
/// EntityFramework application context.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ApplicationContext"/> class.
/// </remarks>
/// <param name="options">Options for data-context.</param>
public class ApplicationContext(
    DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    private const string SchemaName = "whitebox";
    
    public DbSet<ChatConfigurationEntity> ChatConfiguration { get; init; }
    
    public DbSet<UserEntity> User { get; init; }
    
    public DbSet<ConversationEntity> Conversation { get; init; }
    
    public DbSet<MessageEntity> Message { get; init; }
    
    public DbSet<ContentEntity> Content { get; init; }
    
    public DbSet<PromptEntity> Prompt { get; init; }
    
    public DbSet<UsageEntity> Usage { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaName);

        modelBuilder.Entity<ChatConfigurationEntity>(ChatConfigurationEntity.OnModelCreating);
        modelBuilder.Entity<UserEntity>(UserEntity.OnModelCreating);
        
        modelBuilder.Entity<ConversationEntity>(ConversationEntity.OnModelCreating);
        modelBuilder.Entity<MessageEntity>(MessageEntity.OnModelCreating);
        modelBuilder.Entity<ContentEntity>(ContentEntity.OnModelCreating);
        
        modelBuilder.Entity<PromptEntity>(PromptEntity.OnModelCreating);
        modelBuilder.Entity<UsageEntity>(UsageEntity.OnModelCreating);
        
        base.OnModelCreating(modelBuilder);
    }
}