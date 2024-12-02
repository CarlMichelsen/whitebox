using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Entity.Util;

public static class EntityFrameworkTypedGuidExtensions
{
    public static void RegisterTypedIdConversion<TProperty>(
        this PropertyBuilder<TProperty> propertyBuilder,
        Expression<Func<Guid, TProperty>> convertTo)
        where TProperty : TypedGuid<TProperty>
    {
        propertyBuilder
            .HasConversion(id => id.Value, convertTo)
            .HasColumnType("uuid");
    }
}