using Clean.Modules.Crm.Dto.Commands.Orders;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Orders.Features.DeleteOrder;
internal class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
    }
}