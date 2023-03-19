using FluentValidation;

namespace Clean.Modules.Crm.Application.Orders.CancelOrder;
internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
    }
}