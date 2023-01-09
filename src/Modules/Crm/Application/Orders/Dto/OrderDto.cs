namespace Clean.Modules.Crm.Application.Orders.Dto;
public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    List<OrderItemDto> OrderItems);