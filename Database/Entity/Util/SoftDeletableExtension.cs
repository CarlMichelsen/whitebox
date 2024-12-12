using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity.Util;

public static class SoftDeletableExtension
{
    public static void MakeSoftDeletable<TEntity>(this EntityTypeBuilder<TEntity> entity, ModelBuilder modelBuilder)
        where TEntity : class, ISoftDeletable
    {
        entity.HasQueryFilter(e => e.DeletedAtUtc == null);

        var entityType = modelBuilder.Model.FindEntityType(typeof(TEntity))!;
        var deletedAtColumn = entityType.FindProperty(nameof(ISoftDeletable.DeletedAtUtc))!;
        entity
            .HasIndex(p => p.DeletedAtUtc)
            .HasFilter($"\"{entityType.GetTableName()}\".\"{deletedAtColumn.GetColumnName()}\" IS NULL");
    }
}