using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Infrastructure.DomainEventTypeMapping;
using Clean.Modules.Shared.Infrastructure.Idempotency;
using Clean.Modules.Shared.Infrastructure.Services;
using Clean.Modules.UserAccess.Application.Interfaces.Persistence;
using Clean.Modules.UserAccess.Application.Interfaces.Services;
using Clean.Modules.UserAccess.Infrastructure.Services.Jwt;
using Clean.Modules.UserAccess.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Clean.Modules.UserAccess.Infrastructure.Setup;

internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        var jwtOptions = config.GetJwtOptions();

        services.AddSingleton(Options.Create(jwtOptions));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.DecorateEventHandlersWithIdempotency();

        services.AddSingleton<IDomainEventTypeMapping>(_ =>
            new DomainEventTypeMapping(typeof(Domain.AssemblyMarker).Assembly));

        return services;
    }
}