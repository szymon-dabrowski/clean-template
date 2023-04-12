using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.DeleteOrder;
internal class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(new OrderId(request.OrderId));

        order?.Delete();
    }
}