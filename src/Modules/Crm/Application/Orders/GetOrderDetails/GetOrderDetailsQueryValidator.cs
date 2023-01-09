using FluentValidation;

namespace Clean.Modules.Crm.Application.Orders.GetOrderDetails;
internal class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
{
    public GetOrderDetailsQueryValidator()
    {
        RuleFor(q => q.OrderId).NotEmpty();
    }
}