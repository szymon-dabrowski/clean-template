using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Infrastructure.Services;
using Clean.Modules.UserAccess.Application;
using Clean.Modules.UserAccess.Application.Interfaces.Persistance;
using Clean.Modules.UserAccess.Infrastructure.Persistance.User;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Clean.Modules.UserAccess.Infrastructure.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddUserAccessModule(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddMediatR(typeof(Application.AssemblyMarker));

        services.AddAuth(config);

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
