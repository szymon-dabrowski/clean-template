using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Dto.Queries.Items.Model;
using Mapster;

namespace Clean.Modules.Crm.Application.Items.Mappings;
internal class ItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Item, ItemDto>();
    }
}