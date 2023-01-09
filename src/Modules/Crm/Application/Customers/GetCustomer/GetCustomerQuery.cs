using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Customers.GetCustomer;
public record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerDto?>;