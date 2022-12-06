using Quartz;

namespace Clean.Modules.Shared.Infrastructure.Outbox;

public static class OutboxProcessingJobExtensions
{
    private const int OutboxProcessingInterval = 10;

    public static IServiceCollectionQuartzConfigurator ConfigureOutboxProcessingJob<TOutboxProcessingJob>(
        this IServiceCollectionQuartzConfigurator config)
        where TOutboxProcessingJob : OutboxProcessingJob
    {
        var jobKey = new JobKey(nameof(TOutboxProcessingJob));

        return config
            .AddJob<TOutboxProcessingJob>(jobKey)
            .AddTrigger(trigger => trigger
                .ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(OutboxProcessingInterval)
                    .RepeatForever()));
    }
}