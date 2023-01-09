using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Orders.GetOrder;
internal class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto?>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetOrderQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<OrderDto?> Handle(
        GetOrderQuery request,
        CancellationToken cancellationToken)
    {
        var order = await dbContext.Set<Order>()
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        return order == null
            ? null
            : mapper.Map<OrderDto>(order);
    }
}