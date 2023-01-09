using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Domain.Items;
using Mapster;

namespace Clean.Modules.Crm.Application.Items.Mappings;
internal class ItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Item, ItemDto>();
    }
}