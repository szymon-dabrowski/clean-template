using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Infrastructure.DependencyInjection;
using Clean.Modules.Shared.Infrastructure.ExecutionContext;
using Clean.Modules.Shared.Infrastructure.Services;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Clean.Modules.UserAccess.Domain.Users.Services;
using Clean.Modules.UserAccess.Infrastructure.Setup.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Infrastructure.Setup;

internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config,
        IExecutionContextAccessor executionContextAccessor)
    {
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.Jwt));

        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton(executionContextAccessor);

        services
            .RegisterCommandHandlersAsClosedTypes(typeof(Application.AssemblyMarker).Assembly)
            .DecorateCommandHandlersWithUnitOfWork();

        // TODO - add some scanning for domain services
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IPasswordHashing, PasswordHashing>();
        services.AddScoped<IPasswordStrengthChecker, PasswordStrengthChecker>();
        services.AddScoped<IUserEmailUniquenessChecker, UserEmailUniquenessChecker>();

        return services;
    }
}