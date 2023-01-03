using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
using Clean.Modules.Shared.Persistence.Outbox;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Quartz;
using Xunit;

namespace Clean.Modules.Shared.IntegrationTests.Idempotency;

public class IdempotencyEventHandlerTests
{
    [Fact]
    public async Task DomainEventHandle_ShouldPopulate_OutboxMessageHandleTable()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<DbContext>();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var outboxProcessingJob = new TestOutboxProcessingJob(new TestModuleServiceProvider(serviceProvider));

        var testAggregate = TestAggregateRoot.Create("TestPropertyValue");
        dbContext.Set<TestAggregateRoot>().Add(testAggregate);
        await unitOfWork.Commit();

        await outboxProcessingJob.Execute(new Mock<IJobExecutionContext>().Object);

        Assert.Single(dbContext.Set<OutboxMessageHandle>());

        var handleInfo = dbContext.Set<OutboxMessageHandle>().First();
        Assert.Equal(nameof(TestAggregateRootCreatedDomainEventHandler), handleInfo.HandledBy);
    }

    [Fact]
    public async Task DomainEventHandle_ShouldProcess_TheSameEventOnlyOnce()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<DbContext>();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var inMemoryPubishedEventsStore = serviceProvider.GetRequiredService<List<IDomainEvent>>();
        var outboxProcessingJob = new TestOutboxProcessingJob(new TestModuleServiceProvider(serviceProvider));

        var testAggregate = TestAggregateRoot.Create("TestPropertyValue");
        dbContext.Set<TestAggregateRoot>().Add(testAggregate);
        await unitOfWork.Commit();

        await outboxProcessingJob.Execute(new Mock<IJobExecutionContext>().Object);
        await outboxProcessingJob.Execute(new Mock<IJobExecutionContext>().Object);

        Assert.Single(inMemoryPubishedEventsStore);
    }
}