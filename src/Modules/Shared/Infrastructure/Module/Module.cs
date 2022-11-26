using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Modules.Shared.Infrastructure.Module;
public abstract class Module : IModule
{
    private readonly IModuleServiceProvider moduleServiceProvider;

    public Module(IModuleServiceProvider moduleServiceProvider)
    {
        this.moduleServiceProvider = moduleServiceProvider;
    }

    public async Task ExecuteCommand(ICommand command)
    {
        using var scope = moduleServiceProvider.BeginLifetimeScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
    }

    public async Task<TResponse> ExecuteCommand<TResponse>(ICommand<TResponse> command)
    {
        using var scope = moduleServiceProvider.BeginLifetimeScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(command);
    }

    public async Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query)
    {
        using var scope = moduleServiceProvider.BeginLifetimeScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(query);
    }
}
