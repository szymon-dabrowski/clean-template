using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Items.GetItem;
public record GetItemQuery(Guid ItemId) : IQuery<ItemDto?>;