using Clean.Modules.UserAccess.Infrastructure.Setup;
using Quartz;

namespace Clean.Web.API.Setup;

internal static class QuartzSetup
{
    public static IServiceCollection AddOutboxMessagesProcessingJob(
        this IServiceCollection services)
    {
        services.AddQuartz(config =>
        {
            UserAccesStartup.InitializeBackgroundJobs(config);

            config.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }
}