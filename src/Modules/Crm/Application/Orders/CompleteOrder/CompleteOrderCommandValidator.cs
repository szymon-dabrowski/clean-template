using Clean.Modules.Crm.Application.Orders.CompleteOrder;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Orders.CancelOrder;
internal class CompleteOrderCommandValidator : AbstractValidator<CompleteOrderCommand>
{
    public CompleteOrderCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
    }
}