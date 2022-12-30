using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Shared.Persistence.Outbox;
public static class OutboxMessageEntityConfiguration
{
    public static ModelBuilder ApplyOutboxMessageEntityConfiguration(
        this ModelBuilder modelBuilder, string schemaName)
    {
        modelBuilder.Entity<OutboxMessage>(e =>
        {
            e.HasKey(m => m.Id);

            e.ToTable("OutboxMessages", schemaName);

            e.Property(m => m.Content).IsRequired();

            e.Property(m => m.Type).IsRequired();
        });

        modelBuilder.Entity<OutboxMessageHandle>(e =>
        {
            e.HasKey(m => new { m.Id, m.HandledBy });

            e.ToTable("OutboxMessageHandle", schemaName);
        });

        return modelBuilder;
    }
}