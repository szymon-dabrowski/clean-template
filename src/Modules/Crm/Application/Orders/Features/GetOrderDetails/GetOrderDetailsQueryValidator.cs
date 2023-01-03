using Clean.Modules.Crm.Dto.Queries.Orders;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Orders.Features.GetOrderDetails;
internal class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
{
    public GetOrderDetailsQueryValidator()
    {
        RuleFor(q => q.OrderId).NotEmpty();
    }
}