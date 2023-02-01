using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Infrastructure.DependencyInjection;
using Clean.Modules.Shared.Infrastructure.Permissions;
using Clean.Modules.Shared.Infrastructure.Services;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using Clean.Modules.UserAccess.Domain.Permissions.Services;
using Clean.Modules.UserAccess.Domain.Roles.Services;
using Clean.Modules.UserAccess.Domain.Users.Services;
using Clean.Modules.UserAccess.Infrastructure.Domain.Permissions.Services;
using Clean.Modules.UserAccess.Infrastructure.Domain.Roles.Services;
using Clean.Modules.UserAccess.Infrastructure.Domain.Users.Services;
using Clean.Modules.UserAccess.Infrastructure.Setup.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Infrastructure.Setup;

internal static class DependencyInjection
{
    internal static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config,
        IExecutionContextAccessor executionContextAccessor,
        IEnumerable<IPermissionsEnumeration> permissions)
    {
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.Jwt));

        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton(executionContextAccessor);

        services
            .RegisterCommandHandlersAsClosedTypes(typeof(Application.AssemblyMarker).Assembly)
            .DecorateCommandHandlersWithUnitOfWork();

        services.AddSingleton(permissions);

        // TODO - add some scanning for domain services
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IPasswordHashing, PasswordHashing>();
        services.AddScoped<IPasswordStrengthChecker, PasswordStrengthChecker>();
        services.AddScoped<IUserEmailUniquenessChecker, UserEmailUniquenessChecker>();
        services.AddScoped<IRoleExistenceChecker, RoleExistenceChecker>();

        services.AddScoped<IPermissionExistenceChecker, PermissionExistenceChecker>();

        services.AddScoped<IRoleNameUniquenessChecker, RoleNameUniquenessChecker>();

        return services;
    }
}