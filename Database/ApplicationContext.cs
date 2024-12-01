using Database.Entity;
using Database.Entity.Id;
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
    
    public DbSet<UserEntity> User { get; init; }
    
    public DbSet<ChatConfigurationEntity> ChatConfiguration { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaName);

        ChatConfigurationEntity.OnModelCreating(modelBuilder);
        UserEntity.OnModelCreating(modelBuilder);
    }
}