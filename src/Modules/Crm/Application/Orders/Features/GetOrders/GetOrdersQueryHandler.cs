using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Dto.Queries.Orders;
using Clean.Modules.Crm.Dto.Queries.Orders.Model;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.Features.GetOrders;
internal class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetOrdersQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Order>()
            .Select(o => mapper.Map<OrderDto>(o))
            .ToListAsync(cancellationToken);
    }
}