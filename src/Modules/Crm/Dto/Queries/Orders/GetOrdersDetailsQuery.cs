using Clean.Modules.Crm.Dto.Queries.Orders.Model;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Dto.Queries.Orders;
public record GetOrdersDetailsQuery : IQuery<List<OrderDetailsDto>>;