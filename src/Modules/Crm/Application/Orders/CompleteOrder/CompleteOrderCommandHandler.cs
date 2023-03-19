using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.CompleteOrder;
internal class CompleteOrderCommandHandler : ICommandHandler<CompleteOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository orderRepository;

    public CompleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(request.OrderId);

        if (order == null)
        {
            return Error.EntityNotFound(request.OrderId);
        }

        var completeResult = await order.Complete();

        if (completeResult.IsError)
        {
            return completeResult.Errors.ToArray();
        }

        return Unit.Value;
    }
}