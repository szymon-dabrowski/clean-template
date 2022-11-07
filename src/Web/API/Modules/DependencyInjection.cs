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
}
