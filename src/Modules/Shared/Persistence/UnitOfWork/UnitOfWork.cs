using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext dbContext;
    private readonly IDateTimeProvider dateTimeProvider;

    public UnitOfWork(DbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        this.dbContext = dbContext;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task<int> Commit(CancellationToken cancellationToken = default)
    {
        AddDomainEventsToOutbox();

        UpdateAuditableEntities();

        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    private void AddDomainEventsToOutbox()
    {
        var outboxMessages = dbContext.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(e => e.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.GetDomainEvents();
                aggregateRoot.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage()
            {
                Id = Guid.NewGuid(),
                Content = JsonSerializer.Serialize(
                    domainEvent,
                    domainEvent.GetType()),
                OccuredOn = dateTimeProvider.UtcNow,
                Type = domainEvent.GetType().Name,
            })
            .ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private void UpdateAuditableEntities()
    {
        var entries = dbContext.ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entityEntry in entries)
        {
            UpdateAuditableEntity(entityEntry);
        }
    }

    private void UpdateAuditableEntity(EntityEntry<IAuditableEntity> entityEntry)
    {
        if (entityEntry.State == EntityState.Added)
        {
            entityEntry.Property(e => e.CreatedByUserId)
                .CurrentValue = Guid.NewGuid();

            entityEntry.Property(e => e.CreatedDateUtc)
                .CurrentValue = dateTimeProvider.UtcNow;
        }

        if (entityEntry.State == EntityState.Modified)
        {
            entityEntry.Property(e => e.ModifiedByUserId)
                .CurrentValue = Guid.NewGuid();

            entityEntry.Property(e => e.ModifiedDateUtc)
                .CurrentValue = dateTimeProvider.UtcNow;
        }
    }
}