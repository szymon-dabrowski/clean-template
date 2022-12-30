using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Modules.Shared.Persistence.Setup;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Assembly persistenceAssembly)
    {
        services.Scan(scan => scan
            .FromAssemblies(persistenceAssembly)
            .AddClasses(classes => classes
                .Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}