using Clean.Web.Dto.Crm.Orders.Model;

namespace Clean.Web.Dto.Crm.Orders.Requests;
public record UpdateOrderRequest(
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    IEnumerable<OrderItem> OrderItems);