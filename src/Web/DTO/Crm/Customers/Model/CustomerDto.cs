namespace Clean.Web.Dto.Crm.Customers.Model;
public record CustomerDto(
    Guid Id,
    string Name,
    string TaxId,
    string Address,
    string PostalCode,
    string City,
    List<string> Phones,
    List<string> Emails);