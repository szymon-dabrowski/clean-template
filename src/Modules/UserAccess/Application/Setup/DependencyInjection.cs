using Clean.Modules.Shared.Application.Behaviors;
using Clean.Modules.Shared.Application.Setup;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Application.Setup;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(AssemblyMarker).Assembly;

        services.AddMappings(applicationAssembly);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);

        return services;
    }
}