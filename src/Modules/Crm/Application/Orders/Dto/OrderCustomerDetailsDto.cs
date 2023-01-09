namespace Clean.Modules.Crm.Application.Orders.Dto;
public record OrderCustomerDetailsDto(
    Guid CustomerId,
    string Name,
    string TaxId,
    string Address,
    string PostalCode,
    string City,
    List<string> Phones,
    List<string> Emails);