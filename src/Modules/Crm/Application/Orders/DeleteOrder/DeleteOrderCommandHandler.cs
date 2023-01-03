using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Dto.Commands.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Modules.Crm.Application.Orders.DeleteOrder;
internal class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository orderRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        this.orderRepository = orderRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(request.OrderId);

        order?.Delete();

        return Unit.Value;
    }
}