using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Orders.GetOrders;
public record GetOrdersQuery : IQuery<List<OrderDto>>;