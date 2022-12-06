using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;
using Clean.Modules.Shared.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Quartz;
using System.Text.Json;
using Xunit;

namespace Clean.Modules.Shared.IntegrationTests.OutboxProcessingJob;
public class OutboxProcessingJobTests
{
    [Fact]
    public async Task OutboxProcessingJob_ShouldPublishDomainEvents_WhenOutboxMessageExists()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<DbContext>();
        var inMemoryPubishedEventsStore = serviceProvider.GetRequiredService<List<IDomainEvent>>();
        var outboxProcessingJob = new TestOutboxProcessingJob(new TestModuleServiceProvider(serviceProvider));

        var testId = Guid.NewGuid();
        var testEvent = new TestAggregateRootCreatedDomainEvent(testId, testId.ToString());

        dbContext.Set<OutboxMessage>().Add(new OutboxMessage()
        {
            Id = testEvent.Id,
            Content = JsonSerializer.Serialize(testEvent, testEvent.GetType()),
            OccuredOn = DateTime.UtcNow,
            Type = testEvent.GetType().Name,
        });

        await dbContext.SaveChangesAsync();

        await outboxProcessingJob.Execute(new Mock<IJobExecutionContext>().Object);

        var publishedEvent = inMemoryPubishedEventsStore.First() as TestAggregateRootCreatedDomainEvent;

        Assert.Single(inMemoryPubishedEventsStore);
        Assert.NotNull(publishedEvent);
        Assert.Equal(testId.ToString(), publishedEvent.TestProperty);
        Assert.Equal(testId, publishedEvent.TestAggregateRootId);
    }

    [Fact]
    public async Task OutboxProcessingJob_ShouldUpdateOutboxMessages_AfterProcessing()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<DbContext>();
        var outboxProcessingJob = new TestOutboxProcessingJob(new TestModuleServiceProvider(serviceProvider));

        var testId = Guid.NewGuid();
        var testEvent = new TestAggregateRootCreatedDomainEvent(testId, testId.ToString());

        dbContext.Set<OutboxMessage>().Add(new OutboxMessage()
        {
            Id = testEvent.Id,
            Content = JsonSerializer.Serialize(testEvent, testEvent.GetType()),
            OccuredOn = DateTime.UtcNow,
            Type = testEvent.GetType().Name,
        });

        await dbContext.SaveChangesAsync();

        await outboxProcessingJob.Execute(new Mock<IJobExecutionContext>().Object);

        var outboxMessage = await dbContext.Set<OutboxMessage>().FindAsync(testEvent.Id);

        Assert.NotNull(outboxMessage);
        Assert.NotNull(outboxMessage.ProcessedOn);
        Assert.True(outboxMessage.ProcessedOn > outboxMessage.OccuredOn);
    }

    [Fact]
    public async Task OutboxProcessingJob_ShouldProcess_TheSameMessageOnlyOnece()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<DbContext>();
        var inMemoryPubishedEventsStore = serviceProvider.GetRequiredService<List<IDomainEvent>>();
        var outboxProcessingJob = new TestOutboxProcessingJob(new TestModuleServiceProvider(serviceProvider));

        var testId = Guid.NewGuid();
        var testEvent = new TestAggregateRootCreatedDomainEvent(testId, testId.ToString());

        dbContext.Set<OutboxMessage>().Add(new OutboxMessage()
        {
            Id = testEvent.Id,
            Content = JsonSerializer.Serialize(testEvent, testEvent.GetType()),
            OccuredOn = DateTime.UtcNow,
            Type = testEvent.GetType().Name,
        });

        await dbContext.SaveChangesAsync();

        await outboxProcessingJob.Execute(new Mock<IJobExecutionContext>().Object);
        await outboxProcessingJob.Execute(new Mock<IJobExecutionContext>().Object);

        Assert.Single(inMemoryPubishedEventsStore);
    }
}
