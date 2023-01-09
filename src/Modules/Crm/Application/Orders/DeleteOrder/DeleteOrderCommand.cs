using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Orders.DeleteOrder;
public record DeleteOrderCommand(Guid OrderId) : ICommand;