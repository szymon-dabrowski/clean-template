using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;

internal class TestAggregateRoot : AggregateRoot<Guid>
{
    private TestAggregateRoot(Guid id, string testProperty)
        : base(id)
    {
        TestProperty = testProperty;

        RaiseDomainEvent(new TestAggregateRootCreatedDomainEvent(id, testProperty));
    }

    public string TestProperty { get; set; }

    internal static TestAggregateRoot Create(string testProperty)
    {
        return new TestAggregateRoot(Guid.NewGuid(), testProperty);
    }
}