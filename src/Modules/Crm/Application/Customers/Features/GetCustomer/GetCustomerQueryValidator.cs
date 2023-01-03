using Clean.Modules.Crm.Dto.Queries.Customers;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Customers.Features.GetCustomer;
internal class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(q => q.CustomerId).NotEmpty();
    }
}