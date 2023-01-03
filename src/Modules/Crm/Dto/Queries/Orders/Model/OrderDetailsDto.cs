namespace Clean.Modules.Crm.Dto.Queries.Orders.Model;
public record OrderDetailsDto(
    Guid OrderId,
    string OrderNumber,
    DateTime OrderDate,
    string Currency,
    OrderCustomerDetailsDto Customer,
    List<OrderItemDetailsDto> OrderItems);