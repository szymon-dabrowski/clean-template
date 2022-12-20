using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Crm.Dto.Commands.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Customers;
internal class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ICustomerNameUniquenessChecker customerNameUniquenessChecker;
    private readonly ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker;

    public UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        ICustomerNameUniquenessChecker customerNameUniquenessChecker,
        ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker)
    {
        this.customerRepository = customerRepository;
        this.customerNameUniquenessChecker = customerNameUniquenessChecker;
        this.customerTaxIdUniquenessChecker = customerTaxIdUniquenessChecker;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.CustomerId);

        if (customer == null)
        {
            return Error.EntityNotFound(request.CustomerId);
        }

        var updateResult = await customer.Update(
            request.Name,
            request.TaxId,
            request.Address,
            request.PostalCode,
            request.City,
            request.Phones,
            request.Emails,
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        if (updateResult.IsError)
        {
            return updateResult.Errors.ToArray();
        }

        return Unit.Value;
    }
}