using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Domain;
using Clean.Modules.Shared.Infrastructure.Module;
using Clean.Modules.Shared.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System.Text.Json;

namespace Clean.Modules.Shared.Infrastructure.Outbox;

[DisallowConcurrentExecution]
public abstract class OutboxProcessingJob : IJob
{
    private const int outboxProcessingCount = 20;

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

        await ProcessOutboxMessages(dbContext, dateTimeProvider, publisher, context);
    }

    private static async Task ProcessOutboxMessages(
        DbContext dbContext,
        IDateTimeProvider dateTimeProvider,
        IPublisher publisher,
        IJobExecutionContext context)
    {
        var messages = await dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOn == null)
            .Take(outboxProcessingCount)
            .ToListAsync(context.CancellationToken);

        var processedOn = dateTimeProvider.UtcNow;

        foreach (var message in messages)
        {
            var domainEvent = JsonSerializer.Deserialize<IDomainEvent>(message.Content);

            if (domainEvent == null) continue;

            await publisher.Publish(domainEvent, context.CancellationToken);

            message.ProcessedOn = processedOn;
        }

        await dbContext.SaveChangesAsync(context.CancellationToken);
    }
}
