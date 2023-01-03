﻿using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Dto.Commands.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.DeleteOrder;
internal class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(request.OrderId);

        order?.Delete();

        return Unit.Value;
    }
}