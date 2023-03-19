using Clean.Modules.Crm.Application.Orders.ConfirmOrder;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Orders.CancelOrder;
internal class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
{
    public ConfirmOrderCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
    }
}