using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Customers.Services;
using Clean.Modules.Crm.Dto.Commands.Customers;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Crm.Application.Customers.Features.CreateCustomer;
internal class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, ErrorOr<Guid>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ICustomerNameUniquenessChecker customerNameUniquenessChecker;
    private readonly ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        ICustomerNameUniquenessChecker customerNameUniquenessChecker,
        ICustomerTaxIdUniquenessChecker customerTaxIdUniquenessChecker)
    {
        this.customerRepository = customerRepository;
        this.customerNameUniquenessChecker = customerNameUniquenessChecker;
        this.customerTaxIdUniquenessChecker = customerTaxIdUniquenessChecker;
    }

    public async Task<ErrorOr<Guid>> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await Customer.Create(
            request.Name,
            request.TaxId,
            request.Address,
            request.PostalCode,
            request.City,
            request.Phones,
            request.Emails,
            customerNameUniquenessChecker,
            customerTaxIdUniquenessChecker);

        if (customer.IsError)
        {
            return customer.Errors.ToArray();
        }

        await customerRepository.Add(customer.Value);

        return customer.Value.Id;
    }
}