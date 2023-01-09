using FluentValidation;

namespace Clean.Modules.Crm.Application.Items.GetItem;
internal class GetItemQueryValidator : AbstractValidator<GetItemQuery>
{
    public GetItemQueryValidator()
    {
        RuleFor(q => q.ItemId).NotNull();
    }
}