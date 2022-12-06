using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Shared.Infrastructure.Idempotency;
public class IdempotencyEventHandlerDecorator<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly DbContext dbContext;
    private readonly INotificationHandler<TDomainEvent> decorated;
    private readonly IDateTimeProvider dateTimeProvider;

    public IdempotencyEventHandlerDecorator(
        DbContext dbContext,
        INotificationHandler<TDomainEvent> decorated,
        IDateTimeProvider dateTimeProvider)
    {
        this.dbContext = dbContext;
        this.decorated = decorated;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string handler = decorated.GetType().Name;

        if (await dbContext.Set<OutboxMessageHandle>().AnyAsync(
            outboxMessageConsumer =>
                outboxMessageConsumer.Id == notification.Id &&
                outboxMessageConsumer.HandledBy == handler,
            cancellationToken))
        {
            return;
        }

        await decorated.Handle(notification, cancellationToken);

        dbContext.Set<OutboxMessageHandle>()
            .Add(new OutboxMessageHandle()
            {
                Id = notification.Id,
                HandledBy = handler,
                HandledOn = dateTimeProvider.UtcNow,
            });

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}