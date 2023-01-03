using Clean.Modules.Crm.Dto.Commands.Items;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Items.Features.DeleteItem;
internal class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator()
    {
        RuleFor(c => c.ItemId).NotEmpty();
    }
}