using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Application.Commands.CreateTestAggregateRoot;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Application.Commands.DeleteTestAggregateRoot;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Domain.Events;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Infrastructure;
using Clean.Modules.Shared.IntegrationTests.SeedWork.Persistence;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Xunit;

namespace Clean.Modules.Shared.IntegrationTests.UnitOfWork;

public class UnitOfWorkTests
{
    [Fact]
    public async Task Commit_DomainEventsRaised_DomainEventsStored()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();

        var testAggregate = TestAggregateRoot.Create("TestPropertyValue");
        dbContext.TestAggregateRoots.Add(testAggregate);
        await unitOfWork.Commit();

        var outboxMessages = dbContext.OutboxMessages.ToList();

        Assert.Single(outboxMessages);
    }

    [Fact]
    public async Task Commit_DomainEventsRaised_DomainEventsSerialized()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();

        var testPropertyValue = "TestPropertyValue";
        var testAggregate = TestAggregateRoot.Create(testPropertyValue);
        dbContext.TestAggregateRoots.Add(testAggregate);
        await unitOfWork.Commit();

        var outboxMessage = dbContext.OutboxMessages.First();
        var domainEvent = JsonSerializer
            .Deserialize<TestAggregateRootCreatedDomainEvent>(outboxMessage.Content);

        Assert.NotNull(domainEvent);
        Assert.Equal(nameof(TestAggregateRootCreatedDomainEvent), outboxMessage.Type);
        Assert.Equal(testAggregate.Id, domainEvent.TestAggregateRootId);
        Assert.Equal(testAggregate.TestProperty, domainEvent.TestProperty);
    }

    [Fact]
    public async Task HandleWithResult_CommandHandlerDecorated_ChangesCommitted()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        var testAggregate = await mediator.Send(new CreateTestAggregateRootCommand(TestProperty: "value"));

        Assert.Single(dbContext.TestAggregateRoots);
        Assert.Single(dbContext.TestAggregateRoots.Where(r => r.Id == testAggregate.Value.Id));
    }

    [Fact]
    public async Task Handle_CommandHandlerDecorated_ChangesCommitted()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();
        var mediator = serviceProvider.GetRequiredService<IMediator>();

        var testAggregate = TestAggregateRoot.Create(testProperty: "value");
        dbContext.Add(testAggregate);
        await dbContext.SaveChangesAsync();

        await mediator.Send(new DeleteTestAggregateRootCommand(testAggregate.Id));

        Assert.Empty(dbContext.TestAggregateRoots);
    }

    [Fact]
    public async Task Commit_AuditableEntityCreated_CreatedDateAndCreatedByUserIdValuesSet()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var executionContextAccessor = serviceProvider
            .GetRequiredService<IExecutionContextAccessor>() as TestExecutionContextAccessor;
        var dateTimeProvider = serviceProvider
            .GetRequiredService<IDateTimeProvider>() as TestDateTimeProvider;

        executionContextAccessor!.UserId = Guid.NewGuid();
        dateTimeProvider!.UtcNow = DateTime.UtcNow;

        var testAggregate = TestAggregateRoot.Create("TestPropertyValue");
        dbContext.TestAggregateRoots.Add(testAggregate);

        await unitOfWork.Commit();

        var aggregate = dbContext.TestAggregateRoots.Find(testAggregate.Id);

        Assert.Equal(executionContextAccessor.UserId, aggregate!.CreatedByUserId);
        Assert.Equal(dateTimeProvider.UtcNow, aggregate!.CreatedDateUtc);
    }

    [Fact]
    public async Task Commit_AuditableEntityModified_ModifiedDateAndModifiedByUserIdValuesSet()
    {
        var serviceProvider = DependencyInjection.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<TestDbContext>();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var executionContextAccessor = serviceProvider
            .GetRequiredService<IExecutionContextAccessor>() as TestExecutionContextAccessor;
        var dateTimeProvider = serviceProvider
            .GetRequiredService<IDateTimeProvider>() as TestDateTimeProvider;

        executionContextAccessor!.UserId = Guid.NewGuid();
        dateTimeProvider!.UtcNow = DateTime.UtcNow;

        var testAggregate = TestAggregateRoot.Create("TestPropertyValue");
        dbContext.TestAggregateRoots.Add(testAggregate);

        await unitOfWork.Commit();

        var aggregate = dbContext.TestAggregateRoots.Find(testAggregate.Id);
        aggregate!.TestProperty = "UpdateTestPropertyValue";

        await unitOfWork.Commit();

        Assert.Equal(executionContextAccessor.UserId, aggregate.ModifiedByUserId);
        Assert.Equal(dateTimeProvider.UtcNow, aggregate.ModifiedDateUtc);
    }
}