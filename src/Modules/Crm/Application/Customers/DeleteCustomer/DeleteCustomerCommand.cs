using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Customers.DeleteCustomer;
public record DeleteCustomerCommand(Guid CustomerId) : ICommand;