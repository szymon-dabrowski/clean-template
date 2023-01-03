namespace Clean.Modules.Crm.Dto.Queries.Orders.Model;
public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    List<OrderItemDto> OrderItems);