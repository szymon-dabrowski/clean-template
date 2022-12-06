using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;
internal class TestAggregateRootCreatedDomainEvent : DomainEventBase
{
    public Guid TestAggregateRootId { get; }
    public string TestProperty { get; }

    public TestAggregateRootCreatedDomainEvent(Guid testAggregateRootId, string testProperty)
    {
        TestAggregateRootId = testAggregateRootId;
        TestProperty = testProperty;
    }
}
