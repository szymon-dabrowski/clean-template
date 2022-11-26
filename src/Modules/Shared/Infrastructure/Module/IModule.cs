using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Shared.Infrastructure.Module;
public interface IModule
{
    Task ExecuteCommand(ICommand command);
    Task<TResponse> ExecuteCommand<TResponse>(ICommand<TResponse> command);
    Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query);
}