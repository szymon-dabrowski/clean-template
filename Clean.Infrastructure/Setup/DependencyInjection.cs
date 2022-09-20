using Clean.Application.Common.Interfaces.Persistance;
using Clean.Application.Common.Interfaces.Services;
using Clean.Infrastructure.Persistance.User;
using Clean.Infrastructure.Services.DateTimeProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Infrastructure.Setup;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuth(config);

        services.AddSwagger();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
