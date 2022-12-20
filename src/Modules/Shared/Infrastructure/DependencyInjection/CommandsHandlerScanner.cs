using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Modules.Shared.Infrastructure.DependencyInjection;
public static class CommandsHandlerScanner
{
    public static IServiceCollection RegisterCommandHandlersAsClosedTypes(
        this IServiceCollection services,
        params Assembly[] commandHandlerAssambly)
    {
        services.Scan(scan => scan
            .FromAssemblies(commandHandlerAssambly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(ICommandHandler<>))
                .Where(type => !type.IsGenericType))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}