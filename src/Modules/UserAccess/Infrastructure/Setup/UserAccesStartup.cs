using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.UserAccess.Application.Setup;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Services;
using Clean.Modules.UserAccess.Persistence.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Infrastructure.Setup;
public static class UserAccesStartup
{
    public static void Initialize(
        IConfiguration configuration,
        IExecutionContextAccessor executionContextAccessor,
        IEnumerable<IPermissionsModule> permissionsModules,
        Action<DbContextOptionsBuilder>? dbContextOptionsFactoryBuilder = null)
    {
        var services = new ServiceCollection()
            .AddApplication()
            .AddPersistence(configuration, dbContextOptionsFactoryBuilder)
            .AddInfrastructure(configuration, executionContextAccessor, permissionsModules);

        UserAccessServiceProvider.Build(services);
    }
}