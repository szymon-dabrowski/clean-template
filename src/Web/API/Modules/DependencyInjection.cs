using Clean.Modules.Shared.Infrastructure;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Setup;

namespace Clean.Web.API.Modules;

internal static class DependencyInjection
{
    internal static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddUserAccessModule(configuration);

        return services;
    }

    private static void AddUserAccessModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        UserAccesStartup.Initialize(configuration);

        services.AddTransient<IModuleServiceProvider, UserAccessServiceProvider>();
        services.AddTransient<IUserAccessModule>(_ =>
            new UserAccessModule(new UserAccessServiceProvider()));
    }
}
