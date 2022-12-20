using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Dto.Commands.Customers;
public record UpdateCustomerCommand(
    Guid CustomerId,
    string Name,
    string TaxId,
    string Address,
    string PostalCode,
    string City,
    List<string> Phones,
    List<string> Emails)
    : ICommand<ErrorOr<Unit>>;