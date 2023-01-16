using Clean.Modules.Crm.Application.Setup;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Outbox;
using Clean.Modules.Crm.Persistence.Setup;
using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Clean.Modules.Crm.Infrastructure.Setup;
public static class CrmStartup
{
    public static void Initialize(
        IConfiguration configuration,
        IExecutionContextAccessor executionContextAccessor,
        Action<DbContextOptionsBuilder>? dbContextOptionsFactoryBuilder = null)
    {
        var services = new ServiceCollection()
            .AddApplication()
            .AddPersistence(configuration, dbContextOptionsFactoryBuilder)
            .AddInfrastructure(executionContextAccessor);

        CrmServiceProvider.Build(services);
    }

    public static void InitializeBackgroundJobs(IServiceCollectionQuartzConfigurator config)
    {
        config.ConfigureOutboxProcessingJob<CrmModuleOutboxProcessingJob>();
    }
}