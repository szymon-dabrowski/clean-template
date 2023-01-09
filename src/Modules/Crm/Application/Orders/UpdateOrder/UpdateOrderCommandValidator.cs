using FluentValidation;

namespace Clean.Modules.Crm.Application.Orders.UpdateOrder;
internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.OrderNumber).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Currency).Length(3).NotEmpty();
        RuleFor(c => c.OrderDate).GreaterThan(new DateTime(1970, 1, 1));
        RuleFor(c => c.OrderItems).NotEmpty();
        RuleForEach(c => c.OrderItems)
            .NotNull()
            .ChildRules(oi =>
            {
                oi.RuleFor(i => i.ItemId).NotEmpty();
                oi.RuleFor(i => i.Quantity).GreaterThan(0);
                oi.RuleFor(i => i.PricePerUnit).GreaterThanOrEqualTo(0);
            });
    }
}