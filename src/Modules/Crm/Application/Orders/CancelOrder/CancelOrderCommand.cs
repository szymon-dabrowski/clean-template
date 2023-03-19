using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Orders.CancelOrder;
public record CancelOrderCommand(Guid OrderId) : ICommand<ErrorOr<Unit>>;