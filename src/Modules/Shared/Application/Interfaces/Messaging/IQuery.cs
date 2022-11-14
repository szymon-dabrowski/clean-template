using MediatR;

namespace Clean.Modules.Shared.Application.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}
