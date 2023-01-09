namespace Clean.Modules.Crm.Application.Customers.Dto;
public record CustomerDto(
    Guid Id,
    string Name,
    string TaxId,
    string Address,
    string PostalCode,
    string City,
    List<string> Phones,
    List<string> Emails);