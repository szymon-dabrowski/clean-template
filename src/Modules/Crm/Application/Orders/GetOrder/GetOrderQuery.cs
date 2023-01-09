using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Orders.GetOrder;
public record GetOrderQuery(Guid OrderId) : IQuery<OrderDto?>;