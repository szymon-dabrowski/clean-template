using FluentValidation;

namespace Clean.Modules.Crm.Application.Items.DeleteItem;
internal class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandValidator()
    {
        RuleFor(c => c.ItemId).NotEmpty();
    }
}