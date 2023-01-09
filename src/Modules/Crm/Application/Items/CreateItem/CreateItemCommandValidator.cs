using FluentValidation;

namespace Clean.Modules.Crm.Application.Items.CreateItem;
internal class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.BaseCurrency).Length(3);
    }
}