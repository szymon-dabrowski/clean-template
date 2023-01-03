using Clean.Modules.Shared.Application.Behaviors;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Crm.Application.Setup;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(AssemblyMarker).Assembly;

        services.AddMappings();

        services.AddMediatR(applicationAssembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);

        TypeAdapterConfig.GlobalSettings.Scan(applicationAssembly);

        return services;
    }
}