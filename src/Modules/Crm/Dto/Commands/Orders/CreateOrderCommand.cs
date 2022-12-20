using Clean.Modules.Crm.Dto.Commands.Orders.Model;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Crm.Dto.Commands.Orders;
public record CreateOrderCommand(
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    IEnumerable<OrderItem> OrderItems)
    : ICommand<ErrorOr<Guid>>;