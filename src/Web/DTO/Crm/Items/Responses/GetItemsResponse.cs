using Clean.Web.Dto.Crm.Items.Model;

namespace Clean.Web.Dto.Crm.Items.Responses;
public record GetItemsResponse(List<ItemDto> Items);