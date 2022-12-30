namespace Clean.Web.Dto.Crm.Orders.Model;
public record OrderItem(
    Guid ItemId,
    decimal Quantity,
    decimal PricePerUnit);