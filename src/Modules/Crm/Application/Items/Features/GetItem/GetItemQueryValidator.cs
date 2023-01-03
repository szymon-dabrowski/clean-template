using Clean.Modules.Crm.Dto.Queries.Items;
using FluentValidation;

namespace Clean.Modules.Crm.Application.Items.Features.GetItem;
internal class GetItemQueryValidator : AbstractValidator<GetItemQuery>
{
    public GetItemQueryValidator()
    {
        RuleFor(q => q.ItemId).NotNull();
    }
}