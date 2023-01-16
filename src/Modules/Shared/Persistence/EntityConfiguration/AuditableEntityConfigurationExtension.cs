using Clean.Modules.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Modules.Shared.Persistence.EntityConfiguration;
public static class AuditableEntityConfigurationExtension
{
    public static EntityTypeBuilder<TEntity> HasAudit<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAuditableEntity
    {
        builder.Property(e => e.CreatedByUserId)
            .IsRequired();

        builder.Property(e => e.CreatedDateUtc)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(e => e.ModifiedByUserId);

        builder.Property(e => e.ModifiedDateUtc)
            .HasColumnType("datetime2");

        return builder;
    }
}