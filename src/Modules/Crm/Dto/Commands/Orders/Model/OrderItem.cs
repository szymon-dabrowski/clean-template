namespace Clean.Modules.Crm.Dto.Commands.Orders.Model;
public record OrderItem(
    Guid ItemId,
    decimal Quantity,
    decimal PricePerUnit);