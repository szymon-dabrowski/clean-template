using Clean.Modules.Crm.Application.Items.CreateItem;
using Clean.Modules.Crm.Application.Items.DeleteItem;
using Clean.Modules.Crm.Application.Items.GetItem;
using Clean.Modules.Crm.Application.Items.GetItems;
using Clean.Modules.Crm.Application.Items.UpdateItem;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Web.Api.Common.Endpoints;
using Clean.Web.Api.Common.Errors;
using Clean.Web.Dto.Crm.Items.Requests;
using Clean.Web.Dto.Crm.Items.Responses;
using Mapster;

namespace Clean.Web.Api.Modules.Crm.Items;

internal class ItemsEndpoints : IEndpointsModule
{
    private const string ItemsRoute = "/items";

    public void RegisterEndpoints(WebApplication app)
    {
        app.MapGet(ItemsRoute, async (ICrmModule crmModule) =>
        {
            return await crmModule.ExecuteQuery(new GetItemsQuery());
        });

        app.MapGet($"{ItemsRoute}/{{itemId}}", async (ICrmModule crmModule, Guid itemId) =>
        {
            return await crmModule.ExecuteQuery(new GetItemQuery(itemId));
        });

        app.MapPost(ItemsRoute, async (ICrmModule crmModule, CreateItemRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.Adapt<CreateItemCommand>());

            return result.Match(
                itemId => Results.Ok(new CreateItemResponse(itemId)),
                errors => errors.AsProblem());
        });

        app.MapPut($"{ItemsRoute}/{{itemId}}", async (
            ICrmModule crmModule,
            Guid itemId,
            UpdateItemRequest request) =>
        {
            var result = await crmModule.ExecuteCommand(
                request.BuildAdapter()
                    .AddParameters(ItemsMappingConfig.ItemIdParam, itemId)
                    .AdaptToType<UpdateItemCommand>());

            return result.Match(
                _ => Results.Ok(),
                errors => errors.AsProblem());
        });

        app.MapDelete($"{ItemsRoute}/{{itemId}}", async (ICrmModule crmModule, Guid itemId) =>
        {
            await crmModule.ExecuteCommand(new DeleteItemCommand(itemId));
        });
    }
}