namespace Clean.Web.Dto.Crm.Items.Requests;
public record CreateItemRequest(
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency);