using Clean.Modules.Shared.Domain;
using MediatR;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;
internal class TestAggregateRootCreatedDomainEventHandler
    : INotificationHandler<TestAggregateRootCreatedDomainEvent>
{
    private readonly List<IDomainEvent> inMemoryDomainEventsStore;

    public TestAggregateRootCreatedDomainEventHandler(List<IDomainEvent> inMemoryDomainEventsStore)
    {
        this.inMemoryDomainEventsStore = inMemoryDomainEventsStore;
    }

    public Task Handle(TestAggregateRootCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        inMemoryDomainEventsStore.Add(notification);

        return Task.CompletedTask;
    }
}