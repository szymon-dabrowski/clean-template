using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Setup;
using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Setup;
using Clean.Web.Api.Modules.Crm.Items;
using Clean.Web.Api.Modules.UserAccess.Users;

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

        UserAccesStartup.Initialize(app.Configuration, executionContextAccessor);

        CrmStartup.Initialize(app.Configuration, executionContextAccessor);

        return app;
    }
}