using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Shared.Application.Interfaces.Messaging;

public interface ICommand : IRequest<Unit>
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
}
