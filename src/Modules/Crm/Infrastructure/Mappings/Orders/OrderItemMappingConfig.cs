using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Modules.Crm.Infrastructure.Mappings.Orders;
internal class OrderItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Dto.Commands.Orders.Model.OrderItem, Domain.Orders.OrderItem>();
    }
}