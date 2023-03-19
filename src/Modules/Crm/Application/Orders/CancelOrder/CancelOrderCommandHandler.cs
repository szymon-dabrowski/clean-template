using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.CancelOrder;
internal class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository orderRepository;

    public CancelOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(request.OrderId);

        if (order == null)
        {
            return Error.EntityNotFound(request.OrderId);
        }

        var cancelResult = await order.Cancel();

        if (cancelResult.IsError)
        {
            return cancelResult.Errors.ToArray();
        }

        return Unit.Value;
    }
}