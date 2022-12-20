using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Setup;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Setup;

namespace Clean.Web.Api.Modules;

internal static class DependencyInjection
{
    internal static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddUserAccessModule(configuration)
            .AddCrmModule();

        return services;
    }

    private static IServiceCollection AddUserAccessModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        UserAccesStartup.Initialize(configuration);

        // TODO - verify if should be removed
        //services.AddTransient<IModuleServiceProvider, UserAccessServiceProvider>();
        services.AddTransient<IUserAccessModule>(_ =>
            new UserAccessModule(new UserAccessServiceProvider()));

        return services;
    }

    private static IServiceCollection AddCrmModule(this IServiceCollection services)
    {
        CrmStartup.Initialize();

        // TODO - verify if should be removed
        //services.AddTransient<IModuleServiceProvider, CrmServiceProvider>();
        services.AddTransient<ICrmModule>(_ =>
            new CrmModule(new CrmServiceProvider()));

        return services;
    }
}