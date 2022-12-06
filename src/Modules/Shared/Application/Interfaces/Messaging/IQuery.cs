using MediatR;

namespace Clean.Modules.Shared.Application.Interfaces.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}