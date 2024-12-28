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
    public const string SchemaName = "white_box";
    
    public DbSet<ChatConfigurationEntity> ChatConfiguration { get; init; }
    
    public DbSet<UserEntity> User { get; init; }
    
    public DbSet<ConversationEntity> Conversation { get; init; }
    
    public DbSet<MessageEntity> Message { get; init; }
    
    public DbSet<ContentEntity> Content { get; init; }
    
    public DbSet<PromptEntity> Prompt { get; init; }
    
    public DbSet<UsageEntity> Usage { get; init; }
    
    public DbSet<RedirectEntity> Redirect { get; init; }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var softDeleteEntries = this.ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);
        
        foreach (var entityEntry in softDeleteEntries)
        {
            entityEntry.State = EntityState.Modified;
            entityEntry.Property(nameof(ISoftDeletable.DeletedAtUtc)).CurrentValue = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(SchemaName);

        modelBuilder.Entity<ChatConfigurationEntity>(ChatConfigurationEntity.OnModelCreating);
        modelBuilder.Entity<UserEntity>(UserEntity.OnModelCreating);
        
        modelBuilder.Entity<ConversationEntity>(entity => ConversationEntity.OnModelCreating(entity, modelBuilder));
        modelBuilder.Entity<MessageEntity>(entity => MessageEntity.OnModelCreating(entity, modelBuilder));
        modelBuilder.Entity<ContentEntity>(entity => ContentEntity.OnModelCreating(entity, modelBuilder));
        
        modelBuilder.Entity<PromptEntity>(PromptEntity.OnModelCreating);
        modelBuilder.Entity<UsageEntity>(UsageEntity.OnModelCreating);
        
        modelBuilder.Entity<RedirectEntity>(RedirectEntity.OnModelCreating);
        
        base.OnModelCreating(modelBuilder);
    }
}