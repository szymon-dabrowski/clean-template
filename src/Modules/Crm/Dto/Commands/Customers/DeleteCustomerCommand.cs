using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Dto.Commands.Customers;
public record DeleteCustomerCommand(Guid CustomerId) : ICommand;