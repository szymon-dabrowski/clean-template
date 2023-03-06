using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public static class UnitOfWorkCommandHandlerDecoratorExtensions
{
    public static IServiceCollection DecorateCommandHandlersWithUnitOfWork(
        this IServiceCollection services)
    {
        services.TryDecorate(
            typeof(IRequestHandler<>),
            typeof(UnitOfWorkCommandHandlerDecorator<>));

        services.TryDecorate(
            typeof(IRequestHandler<,>),
            typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>));

        return services;
    }
}