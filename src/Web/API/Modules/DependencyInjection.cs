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
            .AddCrmModule(configuration);

        return services;
    }

    private static IServiceCollection AddUserAccessModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        UserAccesStartup.Initialize(configuration);

        services.AddTransient<IUserAccessModule>(_ =>
            new UserAccessModule(new UserAccessServiceProvider()));

        return services;
    }

    private static IServiceCollection AddCrmModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        CrmStartup.Initialize(configuration);

        services.AddTransient<ICrmModule>(_ =>
            new CrmModule(new CrmServiceProvider()));

        return services;
    }
}