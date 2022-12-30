using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Persistence.Database;
public class CrmContext : DbContext
{
    public CrmContext(DbContextOptions<CrmContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Item> Items { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    public DbSet<OutboxMessageHandle> OutboxMessageHandles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder
            .ApplyConfigurationsFromAssembly(GetType().Assembly)
            .ApplyOutboxMessageEntityConfiguration(Constants.CrmSchemaName);
}