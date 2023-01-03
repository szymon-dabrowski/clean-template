namespace Clean.Modules.Crm.Dto.Commands.Orders.Model;
public record OrderItemDto(
    Guid ItemId,
    decimal Quantity,
    decimal PricePerUnit);