using Clean.Modules.Crm.Application.Setup;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Outbox;
using Clean.Modules.Crm.Persistence.Setup;
using Clean.Modules.Shared.Infrastructure.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Clean.Modules.Crm.Infrastructure.Setup;
public static class CrmStartup
{
    public static void Initialize(IConfiguration configuration)
    {
        var services = new ServiceCollection()
            .AddApplication()
            .AddPersistence(configuration)
            .AddInfrastructure();

        CrmServiceProvider.Build(services);
    }

    public static void InitializeBackgroundJobs(IServiceCollectionQuartzConfigurator config)
    {
        config.ConfigureOutboxProcessingJob<CrmModuleOutboxProcessingJob>();
    }
}