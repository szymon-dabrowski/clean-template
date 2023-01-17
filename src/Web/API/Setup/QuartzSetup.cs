using Clean.Modules.Crm.Infrastructure.Setup;
using Clean.Modules.UserAccess.Infrastructure.Setup;
using Quartz;

namespace Clean.Web.Api.Setup;

internal static class QuartzSetup
{
    public static IServiceCollection AddOutboxMessagesProcessingJob(
        this IServiceCollection services)
    {
        services.AddQuartz(config =>
        {
            CrmStartup.InitializeBackgroundJobs(config);

            config.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }
}