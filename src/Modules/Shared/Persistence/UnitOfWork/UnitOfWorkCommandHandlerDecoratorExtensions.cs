using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.Persistence.UnitOfWork;
public static class UnitOfWorkCommandHandlerDecoratorExtensions
{
    public static IServiceCollection DecorateCommandHandlersWithUnitOfWork(
        this IServiceCollection services)
    {
        services.Decorate(
            typeof(IRequestHandler<,>),
            typeof(UnitOfWorkCommandHandlerDecorator<,>));

        return services;
    }
}