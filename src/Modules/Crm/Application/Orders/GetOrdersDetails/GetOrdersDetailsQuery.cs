using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Orders.GetOrdersDetails;
public record GetOrdersDetailsQuery(IReadOnlyCollection<Guid> OrderIds)
    : IQuery<List<OrderDetailsDto>>;