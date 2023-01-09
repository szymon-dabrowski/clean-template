using FluentValidation;

namespace Clean.Modules.Crm.Application.Customers.DeleteCustomer;
internal class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(c => c.CustomerId).NotEmpty();
    }
}