using Clean.Application.Auth.Commands.RegisterUser;
using Clean.Application.Auth.Model;
using Clean.Application.Common.Behaviors;
using Clean.Common.Errors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application.Setup;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
