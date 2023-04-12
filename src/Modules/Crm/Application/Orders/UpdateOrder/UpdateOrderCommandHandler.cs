using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Mapster;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.UpdateOrder;
internal class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IOrderNumberUniquenessChecker orderNumberUniquenessChecker;
    private readonly ICustomerExistenceChecker customerExistenceChecker;
    private readonly IItemExistenceChecker itemExistenceChecker;

    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker)
    {
        this.orderRepository = orderRepository;
        this.orderNumberUniquenessChecker = orderNumberUniquenessChecker;
        this.customerExistenceChecker = customerExistenceChecker;
        this.itemExistenceChecker = itemExistenceChecker;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(new OrderId(request.OrderId));

        if (order == null)
        {
            return Error.EntityNotFound(request.OrderId);
        }

        var updateResult = await order.Update(
            request.CustomerId,
            request.OrderDate,
            request.OrderNumber,
            request.Currency,
            request.OrderItems
                .Select(oi => oi.Adapt<OrderItem>())
                .ToList(),
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker);

        if (updateResult.IsError)
        {
            return updateResult.Errors.ToArray();
        }

        return Unit.Value;
    }
}