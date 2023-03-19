using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;
internal record TestAggregateRootCreatedDomainEvent : DomainEventBase
{
    public TestAggregateRootCreatedDomainEvent(Guid testAggregateRootId, string testProperty)
    {
        TestAggregateRootId = testAggregateRootId;
        TestProperty = testProperty;
    }

    public Guid TestAggregateRootId { get; }

    public string TestProperty { get; }
}