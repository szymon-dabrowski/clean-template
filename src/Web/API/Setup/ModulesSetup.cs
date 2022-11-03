using Clean.Modules.UserAccess.Infrastructure.Setup;

namespace Clean.API.Setup;

internal static class ModulesSetup
{
    internal static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddUserAccessModule(configuration);

        return services;
    }
}
