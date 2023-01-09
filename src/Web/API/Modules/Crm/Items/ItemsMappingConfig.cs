using Clean.Modules.Crm.Application.Items.CreateItem;
using Clean.Modules.Crm.Application.Items.UpdateItem;
using Clean.Web.Dto.Crm.Items.Requests;
using Mapster;

namespace Clean.Web.Api.Modules.Crm.Items;

public class ItemsMappingConfig : IRegister
{
    public const string ItemIdParam = "ItemId";

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateItemRequest, CreateItemCommand>();

        config.NewConfig<UpdateItemRequest, UpdateItemCommand>()
            .Map(
                dest => dest.ItemId,
                _ => (Guid)MapContext.Current!.Parameters[ItemIdParam]);
    }
}