using Clean.Modules.Shared.IntegrationTests.SeedWork;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;
using Clean.Modules.Shared.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Xunit;

namespace Clean.Modules.Shared.IntegrationTests.UnitOfWork;

public class UnitOfWorkTests
{
    [Fact]
    public async Task UnitOfWork_ShouldStoreOutboxMessages_OnCommit()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();

        var testAggregate = TestAggregateRoot.Create("TestPropertyValue");
        dbContext.TestAggregateRoots.Add(testAggregate);
        await unitOfWork.CommitAsync();

        var outboxMessages = dbContext.OutboxMessages.ToList();

        Assert.Single(outboxMessages);
    }

    [Fact]
    public async Task UnitOfWork_ShouldSerializeDomainEvent_OnCommit()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();

        var testPropertyValue = "TestPropertyValue";
        var testAggregate = TestAggregateRoot.Create(testPropertyValue);
        dbContext.TestAggregateRoots.Add(testAggregate);
        await unitOfWork.CommitAsync();

        var outboxMessage = dbContext.OutboxMessages.First();
        var domainEvent = JsonSerializer
            .Deserialize<TestAggregateRootCreatedDomainEvent>(outboxMessage.Content);

        Assert.NotNull(domainEvent);
        Assert.Equal(nameof(TestAggregateRootCreatedDomainEvent), outboxMessage.Type);
        Assert.Equal(testAggregate.Id, domainEvent.TestAggregateRootId);
        Assert.Equal(testAggregate.TestProperty, domainEvent.TestProperty);
    }
}