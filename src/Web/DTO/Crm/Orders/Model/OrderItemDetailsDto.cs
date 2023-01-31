namespace Clean.Web.Dto.Crm.Orders.Model;
public record OrderItemDetailsDto(
    Guid ItemId,
    string Name,
    string Description,
    decimal Quantity,
    decimal BasePrice,
    decimal PricePerUnit,
    string BaseCurrency);