using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Mapster;

namespace Clean.Modules.Crm.Application.Orders.CreateOrder;
internal class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, ErrorOr<Guid>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IOrderNumberUniquenessChecker orderNumberUniquenessChecker;
    private readonly ICustomerExistenceChecker customerExistenceChecker;
    private readonly IItemExistenceChecker itemExistenceChecker;
    private readonly IOrderNumberGenerator orderNumberGenerator;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker,
        IOrderNumberGenerator orderNumberGenerator)
    {
        this.orderRepository = orderRepository;
        this.orderNumberUniquenessChecker = orderNumberUniquenessChecker;
        this.customerExistenceChecker = customerExistenceChecker;
        this.itemExistenceChecker = itemExistenceChecker;
        this.orderNumberGenerator = orderNumberGenerator;
    }

    public async Task<ErrorOr<Guid>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await Order.Create(
            request.CustomerId,
            request.OrderDate,
            request.OrderNumber,
            request.Currency,
            request.OrderItems
                .Select(oi => oi.Adapt<OrderItem>())
                .ToList(),
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        if (order.IsError)
        {
            return order.Errors.ToArray();
        }

        await orderRepository.Add(order.Value);

        return order.Value.Id.Value;
    }
}