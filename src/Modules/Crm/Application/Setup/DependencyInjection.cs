using Clean.Modules.Shared.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Crm.Application.Setup;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(AssemblyMarker).Assembly;

        services.AddMediatR(applicationAssembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
}