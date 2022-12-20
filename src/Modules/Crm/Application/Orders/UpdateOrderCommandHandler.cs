using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Crm.Dto.Commands.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MapsterMapper;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders;
internal class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, ErrorOr<Unit>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IOrderNumberUniquenessChecker orderNumberUniquenessChecker;
    private readonly ICustomerExistenceChecker customerExistenceChecker;
    private readonly IItemExistenceChecker itemExistenceChecker;
    private readonly IMapper mapper;

    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker,
        IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.orderNumberUniquenessChecker = orderNumberUniquenessChecker;
        this.customerExistenceChecker = customerExistenceChecker;
        this.itemExistenceChecker = itemExistenceChecker;
        this.mapper = mapper;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetById(request.OrderId);

        if (order == null)
        {
            return Error.EntityNotFound(request.OrderId);
        }

        var updateResult = await order.Update(
            request.CustomerId,
            request.OrderDate,
            request.OrderNumber,
            request.Currency,
            request.OrderItems.Select(mapper.Map<OrderItem>).ToList(),
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