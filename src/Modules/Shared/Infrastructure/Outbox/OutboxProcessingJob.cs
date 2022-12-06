using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Quartz;
using System.Text.Json;

namespace Clean.Modules.Shared.Infrastructure.Outbox;

[DisallowConcurrentExecution]
public abstract class OutboxProcessingJob : IJob
{
    private const int ProcessingCount = 20;
    private const int DelayIntervalMs = 50;
    private const int RetryCount = 3;

    private readonly IModuleServiceProvider moduleServiceProvider;

    protected OutboxProcessingJob(IModuleServiceProvider moduleServiceProvider)
    {
        this.moduleServiceProvider = moduleServiceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = moduleServiceProvider.BeginLifetimeScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
        var dateTimeProvider = scope.ServiceProvider.GetRequiredService<IDateTimeProvider>();
        var domainEventTypeMapping = scope.ServiceProvider
            .GetRequiredService<IDomainEventTypeMapping>();

        await ProcessOutboxMessages(
            dbContext,
            dateTimeProvider,
            publisher,
            domainEventTypeMapping,
            context);
    }

    private async Task ProcessOutboxMessages(
        DbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        IPublisher publisher,
        IDomainEventTypeMapping domainEventTypeMapping,
        IJobExecutionContext context)
    {
        var messages = await dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOn == null)
            .Take(ProcessingCount)
            .ToListAsync(context.CancellationToken);

        var processedOn = dateTimeProvider.UtcNow;

        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                RetryCount,
                attempt => TimeSpan.FromMilliseconds(attempt * DelayIntervalMs));

        foreach (var message in messages)
        {
            if (JsonSerializer.Deserialize(
                message.Content,
                domainEventTypeMapping.Map(message.Type)) is not IDomainEvent domainEvent)
            {
                continue;
            }

            var result = await retryPolicy.ExecuteAndCaptureAsync(() =>
                publisher.Publish(
                    domainEvent,
                    context.CancellationToken)
            );

            message.Error = result.FinalException?.ToString();
            message.ProcessedOn = processedOn;
        }

        await dbContext.SaveChangesAsync(context.CancellationToken);
    }
}
