using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Persistence.Setup;
using Clean.Modules.Shared.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clean.Modules.Crm.Persistence.Database;
public class CrmContext : DbContext
{
    private readonly IConfiguration? configuration;

    public CrmContext(IConfiguration? configuration)
    {
        this.configuration = configuration;
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);

        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var connectionString = configuration?.GetConnectionString(
            DependencyInjection.CrmConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Crm module connection string is empty!");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }
}