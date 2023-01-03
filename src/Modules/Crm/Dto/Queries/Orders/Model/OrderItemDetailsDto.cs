namespace Clean.Modules.Crm.Dto.Queries.Orders.Model;
public record OrderItemDetailsDto(
    Guid ItemId,
    string Name,
    string Description,
    decimal Quantity,
    decimal BasePrice,
    decimal PricePerUnit,
    string BaseCurrency);