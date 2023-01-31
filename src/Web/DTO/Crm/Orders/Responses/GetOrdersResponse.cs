using Clean.Web.Dto.Crm.Orders.Model;

namespace Clean.Web.Dto.Crm.Orders.Responses;
public record GetOrdersResponse(List<OrderDto> Orders);