using Clean.Modules.Crm.Dto.Commands.Items;
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