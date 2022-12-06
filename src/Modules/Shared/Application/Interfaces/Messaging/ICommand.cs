using MediatR;

namespace Clean.Modules.Shared.Application.Interfaces.Messaging;

public interface ICommand : IRequest<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}