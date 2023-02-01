using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Infrastructure.Permissions;
using Clean.Modules.UserAccess.Application.Setup;
using Clean.Modules.UserAccess.Infrastructure.Module;
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
        IEnumerable<IPermissionsEnumeration> permissions,
        Action<DbContextOptionsBuilder>? dbContextOptionsFactoryBuilder = null)
    {
        var services = new ServiceCollection()
            .AddApplication()
            .AddPersistence(configuration, dbContextOptionsFactoryBuilder)
            .AddInfrastructure(configuration, executionContextAccessor, permissions);

        UserAccessServiceProvider.Build(services);
    }
}