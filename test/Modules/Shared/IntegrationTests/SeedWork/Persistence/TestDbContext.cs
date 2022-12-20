using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using Clean.Modules.Shared.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Persistence;
internal class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
        OutboxMessages = Set<OutboxMessage>();
        OutboxMessageHandles = Set<OutboxMessageHandle>();
        TestAggregateRoots = Set<TestAggregateRoot>();
    }

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public DbSet<OutboxMessageHandle> OutboxMessageHandles { get; set; }

    public DbSet<TestAggregateRoot> TestAggregateRoots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestAggregateRoot>(entity =>
        {
            entity.HasKey(r => r.Id);
        });

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.HasKey(r => r.Id);
        });

        modelBuilder.Entity<OutboxMessageHandle>(entity =>
        {
            entity.HasKey(r => new { r.Id, r.HandledBy });
        });
    }
}