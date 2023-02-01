using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Setup;
using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Infrastructure.Permissions;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Setup;

namespace Clean.Web.Api.Modules;

internal static class DependencyInjection
{
    internal static IServiceCollection AddModules(this IServiceCollection services)
    {
        services.AddTransient<IUserAccessModule>(_ =>
            new UserAccessModule(new UserAccessServiceProvider()));

        services.AddTransient<ICrmModule>(_ =>
            new CrmModule(new CrmServiceProvider()));

        return services;
    }

    internal static WebApplication UseModules(this WebApplication app)
    {
        var executionContextAccessor = app.Services
            .GetRequiredService<IExecutionContextAccessor>();

        var permissionsEnumerations = app.Services
            .GetServices<IPermissionsEnumeration>()
            .SelectMany(e => e.GetAllPermissions());

        UserAccesStartup.Initialize(
            app.Configuration,
            executionContextAccessor,
            permissionsEnumerations);

        CrmStartup.Initialize(app.Configuration, executionContextAccessor);

        return app;
    }
}