using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.Crm.Application.Items.GetItems;
public record GetItemsQuery : IQuery<List<ItemDto>>;