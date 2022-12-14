using Clean.Modules.UserAccess.Application.Setup;
using Clean.Modules.UserAccess.Infrastructure.Module;
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
        // TODO - configure job once UserAccess DbContext is ready
        //config.ConfigureOutboxProcessingJob<UserAccessModuleOutboxProcessingJob>();
    }
}