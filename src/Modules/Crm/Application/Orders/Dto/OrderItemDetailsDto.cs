namespace Clean.Modules.Crm.Application.Orders.Dto;
public record OrderItemDetailsDto(
    Guid ItemId,
    string Name,
    string Description,
    decimal Quantity,
    decimal BasePrice,
    decimal PricePerUnit,
    string BaseCurrency);