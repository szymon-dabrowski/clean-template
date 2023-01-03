using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Crm.Dto.Commands.Items;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;

namespace Clean.Modules.Crm.Application.Items.CreateItem;
internal class CreateItemCommandHandler : ICommandHandler<CreateItemCommand, ErrorOr<Guid>>
{
    private readonly IItemRepository itemRepository;
    private readonly IItemUniquenessChecker itemUniquenessChecker;

    public CreateItemCommandHandler(
        IItemRepository itemRepository,
        IItemUniquenessChecker itemUniquenessChecker)
    {
        this.itemRepository = itemRepository;
        this.itemUniquenessChecker = itemUniquenessChecker;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await Item.Create(
            request.Name,
            request.Description,
            request.BasePrice,
            request.BaseCurrency,
            itemUniquenessChecker);

        if (item.IsError)
        {
            return item.Errors.ToArray();
        }

        await itemRepository.Add(item.Value);

        return item.Value.Id;
    }
}