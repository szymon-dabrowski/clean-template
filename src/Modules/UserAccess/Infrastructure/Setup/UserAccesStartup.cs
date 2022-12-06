using Clean.Modules.Shared.Infrastructure.Outbox;
using Clean.Modules.UserAccess.Application.Setup;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Clean.Modules.UserAccess.Infrastructure.Setup;
public static class UserAccesStartup
{
    public static void Initialize(IConfiguration config)
    {
        var services = new ServiceCollection()
            .AddApplication()
            .AddInfrastructure(config);

        UserAccessServiceProvider.Build(services);
    }

    public static void InitializeBackgroundJobs(IServiceCollectionQuartzConfigurator config)
    {
        config.ConfigureOutboxProcessingJob<UserAccessModuleOutboxProcessingJob>();
    }
}