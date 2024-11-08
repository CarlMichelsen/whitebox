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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaName);
    }
}