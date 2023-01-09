namespace Clean.Modules.Crm.Application.Orders.Dto;
public record OrderDetailsDto(
    Guid OrderId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    OrderCustomerDetailsDto Customer,
    List<OrderItemDetailsDto> OrderItems);