using Clean.Modules.Crm.Dto.Queries.Customers.Model;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Dto.Queries.Customers;
public record GetCustomersQuery : IQuery<List<CustomerDto>>;