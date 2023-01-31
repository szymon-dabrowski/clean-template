namespace Clean.Web.Dto.Crm.Items.Model;
public record ItemDto(
    Guid Id,
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency);