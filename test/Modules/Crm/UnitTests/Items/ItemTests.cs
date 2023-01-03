using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Shared.Common.Errors;
using FluentAssertions;
using Moq;
using Xunit;

namespace Clean.Modules.Crm.UnitTests.Items;
public class ItemTests
{
    [Fact]
    public async Task CreatingItem_WhenItemIsUnique_ShouldReturnCreatedItem()
    {
        var name = "testItem";
        var desc = "testDesc";
        var basePrice = 100;
        var baseCurrency = "eur";

        var itemUniquenessChecker = MockItemUniquenessChecker(withResult: true);

        var item = await Item.Create(
            name,
            desc,
            basePrice,
            baseCurrency,
            itemUniquenessChecker);

        item.IsError.Should().BeFalse();
        item.Value.Should().NotBeNull();
        item.Value.Name.Should().Be(name);
        item.Value.Description.Should().Be(desc);
        item.Value.BasePrice.Should().Be(basePrice);
        item.Value.BaseCurrency.Should().Be(baseCurrency);
    }

    [Fact]
    public async Task CreatingItem_WhenItemNameIsNotUnique_ShouldReturnError()
    {
        var itemUniquenessChecker = MockItemUniquenessChecker(withResult: false);
        var item = await CreateTestItem(itemUniquenessChecker);

        item.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingItem_WhenItemIsUnique_ShouldReturnUpdatedItem()
    {
        var itemUniquenessChecker = MockItemUniquenessChecker(withResult: true);
        var item = await CreateTestItem(itemUniquenessChecker);

        var name = "testItem";
        var desc = "testDesc";
        var basePrice = 77;
        var baseCurrency = "usd";

        await item.Value.Update(
            name,
            desc,
            basePrice,
            baseCurrency,
            itemUniquenessChecker);

        item.IsError.Should().BeFalse();
        item.Value.Should().NotBeNull();
        item.Value.Name.Should().Be(name);
        item.Value.Description.Should().Be(desc);
        item.Value.BasePrice.Should().Be(basePrice);
        item.Value.BaseCurrency.Should().Be(baseCurrency);
    }

    [Fact]
    public async Task UpdatingItem_WhenItemIsDeleted_ShouldReturnErrorAsync()
    {
        var itemUniquenessChecker = MockItemUniquenessChecker(withResult: true);
        var item = await CreateTestItem(itemUniquenessChecker);

        item.Value.Delete();

        var updateResult = await item.Value.Update(
            item.Value.Name,
            item.Value.Description,
            item.Value.BasePrice,
            item.Value.BaseCurrency,
            itemUniquenessChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingItem_WhenNameAlreadyExists_ShouldReturnErrorAsync()
    {
        var successItemUniquenessChecker = MockItemUniquenessChecker(withResult: true);
        var failingItemUniquenessChecker = MockItemUniquenessChecker(withResult: false);
        var item = await CreateTestItem(successItemUniquenessChecker);

        var updateResult = await item.Value.Update(
            name: "NewName",
            item.Value.Description,
            item.Value.BasePrice,
            item.Value.BaseCurrency,
            failingItemUniquenessChecker);

        updateResult.IsError.Should().BeTrue();
    }

    private IItemUniquenessChecker MockItemUniquenessChecker(bool withResult)
    {
        var itemUniquenessChecker = new Mock<IItemUniquenessChecker>();

        itemUniquenessChecker
            .Setup(c => c.IsUnique(It.IsAny<string>()))
            .Returns<string>(_ => Task.FromResult(withResult));

        return itemUniquenessChecker.Object;
    }

    private async Task<ErrorOr<Item>> CreateTestItem(
        IItemUniquenessChecker itemUniquenessChecker)
    {
        var item = await Item.Create(
            name: "test",
            description: "test",
            basePrice: 123,
            baseCurrency: "eur",
            itemUniquenessChecker);

        return item;
    }
}