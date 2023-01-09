using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Orders.GetOrderDetails;
public record GetOrderDetailsQuery(Guid OrderId) : IQuery<OrderDetailsDto?>;