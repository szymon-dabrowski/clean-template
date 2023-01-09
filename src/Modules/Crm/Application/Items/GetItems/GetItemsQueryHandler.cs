using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Items.GetItems;
internal class GetItemsQueryHandler : IQueryHandler<GetItemsQuery, List<ItemDto>>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetItemsQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<ItemDto>> Handle(
        GetItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Item>()
            .Select(i => mapper.Map<ItemDto>(i))
            .ToListAsync(cancellationToken);
    }
}