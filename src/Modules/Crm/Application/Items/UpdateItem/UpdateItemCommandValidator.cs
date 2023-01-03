using Clean.Modules.Crm.Dto.Commands.Items;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Items.UpdateItem;
internal class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.BaseCurrency).Length(3);
    }
}