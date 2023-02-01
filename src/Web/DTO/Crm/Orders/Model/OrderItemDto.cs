namespace Clean.Web.Dto.Crm.Orders.Model;
public record OrderItemDto(
    Guid ItemId,
    decimal Quantity,
    decimal PricePerUnit);