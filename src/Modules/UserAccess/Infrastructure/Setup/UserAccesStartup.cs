using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.UserAccess.Application.Setup;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Persistence.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Infrastructure.Setup;
public static class UserAccesStartup
{
    public static void Initialize(
        IConfiguration config,
        IExecutionContextAccessor executionContextAccessor)
    {
        var services = new ServiceCollection()
            .AddApplication()
            .AddPersistence(config)
            .AddInfrastructure(config, executionContextAccessor);

        UserAccessServiceProvider.Build(services);
    }
}