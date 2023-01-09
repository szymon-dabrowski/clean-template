using Clean.Modules.Crm.Application.Items.CreateItem;
using Clean.Modules.Crm.Application.Items.DeleteItem;
using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Application.Items.GetItem;
using Clean.Modules.Crm.Application.Items.GetItems;
using Clean.Modules.Crm.Application.Items.UpdateItem;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Controllers;
using Clean.Web.Dto.Crm.Items.Requests;
using Clean.Web.Dto.Crm.Items.Responses;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Api.Modules.Crm.Items;

[Route("[controller]")]
public class ItemsController : ApiController
{
    private readonly ICrmModule crmModule;
    private readonly IMapper mapper;

    public ItemsController(ICrmModule crmModule, IMapper mapper)
    {
        this.crmModule = crmModule;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<List<ItemDto>> GetItems()
        => await crmModule.ExecuteQuery(new GetItemsQuery());

    [HttpGet("{itemId}")]
    public async Task<ItemDto?> GetItem(Guid itemId)
        => await crmModule.ExecuteQuery(new GetItemQuery(itemId));

    [HttpPost]
    public async Task<IActionResult> CreateItem(CreateItemRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            mapper.Map<CreateItemCommand>(request));

        return result.Match(
            itemId => Ok(new CreateItemResponse(itemId)),
            errors => Problem(errors));
    }

    [HttpPut("{itemId}")]
    public async Task<IActionResult> UpdateItem(Guid itemId, UpdateItemRequest request)
    {
        var result = await crmModule.ExecuteCommand(
            mapper.From(request)
                .AddParameters(ItemsMappingConfig.ItemIdParam, itemId)
                .AdaptToType<UpdateItemCommand>());

        return result.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    [HttpDelete("{itemId}")]
    public async Task DeleteItem(Guid itemId)
        => await crmModule.ExecuteCommand(new DeleteItemCommand(itemId));
}