using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.CompleteOrder;
public record CompleteOrderCommand(Guid OrderId) : ICommand<ErrorOr<Unit>>;