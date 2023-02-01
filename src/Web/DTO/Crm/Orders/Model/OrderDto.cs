namespace Clean.Web.Dto.Crm.Orders.Model;
public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    List<OrderItemDto> OrderItems);