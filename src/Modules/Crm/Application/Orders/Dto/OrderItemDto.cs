namespace Clean.Modules.Crm.Application.Orders.Dto;
public record OrderItemDto(
    Guid ItemId,
    decimal Quantity,
    decimal PricePerUnit);