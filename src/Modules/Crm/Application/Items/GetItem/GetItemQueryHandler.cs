using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.Crm.Application.Items.GetItem;
internal class GetItemQueryHandler : IQueryHandler<GetItemQuery, ItemDto?>
{
    private readonly DbContext dbContext;
    private readonly IMapper mapper;

    public GetItemQueryHandler(DbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ItemDto?> Handle(
        GetItemQuery request,
        CancellationToken cancellationToken)
    {
        var item = await dbContext.Set<Item>()
            .FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

        return item == null
            ? null
            : mapper.Map<ItemDto>(item);
    }
}