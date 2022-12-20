using Clean.Modules.Crm.Application.Setup;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Outbox;
using Clean.Modules.Shared.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Clean.Modules.Crm.Infrastructure.Setup;
public static class CrmStartup
{
    public static void Initialize()
    {
        var services = new ServiceCollection()
            .AddApplication()
            .AddInfrastructure();

        CrmServiceProvider.Build(services);
    }

    public static void InitializeBackgroundJobs(IServiceCollectionQuartzConfigurator config)
    {
        config.ConfigureOutboxProcessingJob<CrmModuleOutboxProcessingJob>();
    }
}