using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using MediatR;

namespace Clean.Modules.Crm.Application.Items.UpdateItem;
internal class UpdateItemCommandHandler : ICommandHandler<UpdateItemCommand, ErrorOr<Unit>>
{
    private readonly IItemRepository itemRepository;
    private readonly IItemUniquenessChecker itemUniquenessChecker;

    public UpdateItemCommandHandler(
        IItemRepository itemRepository,
        IItemUniquenessChecker itemUniquenessChecker)
    {
        this.itemRepository = itemRepository;
        this.itemUniquenessChecker = itemUniquenessChecker;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await itemRepository.GetById(new ItemId(request.ItemId));

        if (item == null)
        {
            return Error.EntityNotFound(request.ItemId);
        }

        var updateResult = await item.Update(
            request.Name,
            request.Description,
            request.BasePrice,
            request.BaseCurrency,
            itemUniquenessChecker);

        if (updateResult.IsError)
        {
            return updateResult.Errors.ToArray();
        }

        return Unit.Value;
    }
}