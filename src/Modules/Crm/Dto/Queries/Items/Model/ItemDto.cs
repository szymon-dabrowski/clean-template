namespace Clean.Modules.Crm.Dto.Queries.Items.Model;
public record ItemDto(
    Guid Id,
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency);