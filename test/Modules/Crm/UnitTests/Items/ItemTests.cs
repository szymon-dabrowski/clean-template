using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Items.Services;
using Clean.Modules.Crm.Domain.Money;
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
        var price = PriceValueObject.Create(value: 100, currency: "eur");

        var itemUniquenessChecker = MockItemUniquenessChecker(withResult: true);

        var item = await Item.Create(
            name,
            desc,
            price,
            itemUniquenessChecker);

        item.IsError.Should().BeFalse();
        item.Value.Should().NotBeNull();
        item.Value.Name.Should().Be(name);
        item.Value.Description.Should().Be(desc);
        item.Value.BasePrice.Value.Should().Be(price.Value);
        item.Value.BasePrice.Currency.Should().Be(price.Currency);
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
        var price = PriceValueObject.Create(value: 100, currency: "eur");

        await item.Value.Update(
            name,
            desc,
            price,
            itemUniquenessChecker);

        item.IsError.Should().BeFalse();
        item.Value.Should().NotBeNull();
        item.Value.Name.Should().Be(name);
        item.Value.Description.Should().Be(desc);
        item.Value.BasePrice.Value.Should().Be(price.Value);
        item.Value.BasePrice.Currency.Should().Be(price.Currency);
    }

    [Fact]
    public async Task UpdatingItem_WhenItemIsDeleted_ShouldReturnErrorAsync()
    {
        var itemUniquenessChecker = MockItemUniquenessChecker(withResult: true);
        var item = await CreateTestItem(itemUniquenessChecker);

        item.Value.Delete();

        var updateResult = await item.Value.Update(
            name: "item",
            description: "desc",
            basePrice: PriceValueObject.Create(value: 100, currency: "eur"),
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
            name: "item",
            description: "desc",
            basePrice: PriceValueObject.Create(value: 100, currency: "eur"),
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
        var basePrice = PriceValueObject.Create(value: 123, currency: "eur");

        var item = await Item.Create(
            name: "test",
            description: "test",
            basePrice,
            itemUniquenessChecker);

        return item;
    }
}