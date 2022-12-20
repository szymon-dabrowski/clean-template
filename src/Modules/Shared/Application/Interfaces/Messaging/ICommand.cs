using MediatR;

namespace Clean.Modules.Shared.Application.Interfaces.Messaging;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}