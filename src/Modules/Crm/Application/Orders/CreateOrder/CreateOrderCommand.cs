using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Crm.Application.Orders.CreateOrder;
public record CreateOrderCommand(
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    List<OrderItemDto> OrderItems)
    : ICommand<ErrorOr<Guid>>;