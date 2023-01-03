using Clean.Modules.Crm.Dto.Queries.Items.Model;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Dto.Queries.Items;
public record GetItemQuery(Guid ItemId) : IQuery<ItemDto?>;