using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Customers.GetCustomers;
public record GetCustomersQuery : IQuery<List<CustomerDto>>;