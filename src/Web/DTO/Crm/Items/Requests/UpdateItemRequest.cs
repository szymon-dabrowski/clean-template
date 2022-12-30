namespace Clean.Web.Dto.Crm.Items.Requests;
public record UpdateItemRequest(
    Guid ItemId,
    string Name,
    string Description,
    decimal BasePrice,
    string BaseCurrency);