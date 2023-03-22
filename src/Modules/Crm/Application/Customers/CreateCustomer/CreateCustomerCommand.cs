using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Crm.Application.Customers.CreateCustomer;

public record CreateCustomerCommand(
    string Name,
    string TaxId,
    string Address,
    string PostalCode,
    string City,
    List<string> Phones,
    List<string> Emails)
    : ICommand<ErrorOr<CustomerId>>;