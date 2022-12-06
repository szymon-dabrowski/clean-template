using Clean.Modules.Shared.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.UserAccess.Application.Setup;
internal static class DependencyInjection
{
    internal static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(AssemblyMarker).Assembly;

        services.AddMediatR(applicationAssembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(applicationAssembly);

        return services;
    }
}