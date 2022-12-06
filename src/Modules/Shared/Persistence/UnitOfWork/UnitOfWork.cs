using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
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

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        AddDomainEventsToOutbox();

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
}