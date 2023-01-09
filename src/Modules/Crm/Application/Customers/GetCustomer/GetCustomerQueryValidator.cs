using FluentValidation;

namespace Clean.Modules.Crm.Application.Customers.GetCustomer;
internal class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(q => q.CustomerId).NotEmpty();
    }
}