using Clean.Web.Dto.Crm.Orders.Model;

namespace Clean.Web.Dto.Crm.Orders.Requests;
public record CreateOrderRequest(
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    IEnumerable<OrderItemDto> OrderItems);