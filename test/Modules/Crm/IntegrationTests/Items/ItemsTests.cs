﻿using Clean.Modules.Crm.Application.Items.CreateItem;
using Clean.Modules.Crm.Application.Items.DeleteItem;
using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Application.Items.GetItem;
using Clean.Modules.Crm.Application.Items.GetItems;
using Clean.Modules.Crm.Application.Items.UpdateItem;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.IntegrationTests.SeedWork;
using FluentAssertions;
using Xunit;

namespace Clean.Modules.Crm.IntegrationTests.Items;
public class ItemsTests : IClassFixture<CrmStartupFixture>
{
    private readonly CrmModule crmModule;

    public ItemsTests(CrmStartupFixture crmStartupFixture)
    {
        crmModule = crmStartupFixture.CrmModule;
    }

    [Fact]
    public async Task CreateItem_ItemDataValid_ItemCreated()
    {
        var expectedItem = new ItemDto(
            Id: Guid.Empty,
            Name: "Item",
            Description: "Item description",
            BasePrice: 100,
            BaseCurrency: "USD");

        var result = await crmModule.ExecuteCommand(new CreateItemCommand(
            expectedItem.Name,
            expectedItem.Description,
            expectedItem.BasePrice,
            expectedItem.BaseCurrency));

        var items = await crmModule.ExecuteQuery(new GetItemsQuery());
        var item = items.First(i => i.Id == result.Value);

        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeEmpty();
        items.Should().NotBeEmpty();
        item.ShouldBe(expectedItem);
    }

    [Fact]
    public async Task UpdateItem_ItemDataValid_ItemUpdated()
    {
        var result = await crmModule.ExecuteCommand(new CreateItemCommand(
            Name: "Item to update",
            Description: "Item to update description",
            BasePrice: 100,
            BaseCurrency: "USD"));

        var expectedItem = new ItemDto(
            Id: result.Value,
            Name: "Updated item",
            Description: "Updated item description",
            BasePrice: 101,
            BaseCurrency: "EUR");

        var updateResult = await crmModule.ExecuteCommand(new UpdateItemCommand(
            result.Value,
            expectedItem.Name,
            expectedItem.Description,
            expectedItem.BasePrice,
            expectedItem.BaseCurrency));

        var item = await crmModule.ExecuteQuery(new GetItemQuery(result.Value));

        updateResult.IsError.Should().BeFalse();

        item.ShouldBe(expectedItem);
    }

    [Fact]
    public async Task DeleteItem_ItemExists_ItemDeleted()
    {
        var result = await crmModule.ExecuteCommand(new CreateItemCommand(
            Name: "Item to delete",
            Description: "Item to delete description",
            BasePrice: 100,
            BaseCurrency: "USD"));

        await crmModule.ExecuteCommand(new DeleteItemCommand(result.Value));

        var item = await crmModule.ExecuteQuery(new GetItemQuery(result.Value));

        item.Should().BeNull();
    }
}