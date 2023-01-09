using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.UpdateOrder;
public record UpdateOrderCommand(
    Guid OrderId,
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    List<OrderItemDto> OrderItems)
    : ICommand<ErrorOr<Unit>>;