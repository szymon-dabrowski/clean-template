using MediatR;

namespace Clean.Modules.Shared.Application.Interfaces.Messaging;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}