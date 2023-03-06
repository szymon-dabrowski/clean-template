using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Shared.Application.Interfaces.Messaging;
using MediatR;

namespace Clean.Modules.Crm.Application.Items.DeleteItem;
internal class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
{
    private readonly IItemRepository itemRepository;

    public DeleteItemCommandHandler(IItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var item = await itemRepository.GetById(request.ItemId);

        item?.Delete();
    }
}