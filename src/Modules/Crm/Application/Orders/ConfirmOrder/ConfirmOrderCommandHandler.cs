using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.ConfirmOrder;
internal class ConfirmOrderCommandHandler : ICommandHandler<ConfirmOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository orderRepository;

    public ConfirmOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(new OrderId(request.OrderId));

        if (order == null)
        {
            return Error.EntityNotFound(request.OrderId);
        }

        var confirmResult = await order.Confirm();

        if (confirmResult.IsError)
        {
            return confirmResult.Errors.ToArray();
        }

        return Unit.Value;
    }
}