namespace Clean.Modules.Crm.Dto.Queries.Orders.Model;
public record OrderItemDto(
    Guid ItemId,
    decimal Quantity,
    decimal PricePerUnit);