using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;

namespace Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;

internal class TestAggregateRoot : AggregateRoot<Guid>
{
    public string TestProperty { get; set; }

    private TestAggregateRoot(Guid id, string testProperty) : base(id)
    {
        TestProperty = testProperty;

        RaiseDomainEvent(new TestAggregateRootCreatedDomainEvent(id, testProperty));
    }

    internal static TestAggregateRoot Create(string testProperty)
    {
        return new TestAggregateRoot(Guid.NewGuid(), testProperty);
    }
}
