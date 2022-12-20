using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Dto.Commands.Orders;
public record DeleteOrderCommand(Guid OrderId) : ICommand;