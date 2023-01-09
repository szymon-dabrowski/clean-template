namespace Clean.Modules.Crm.Application.Items.Dto;
public record ItemDto(
    Guid Id,
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency);