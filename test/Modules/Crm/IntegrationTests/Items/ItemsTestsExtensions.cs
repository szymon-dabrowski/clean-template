using Clean.Modules.Crm.Application.Items.Dto;
using FluentAssertions;

namespace Clean.Modules.Crm.IntegrationTests.Items;
internal static class ItemsTestsExtensions
{
    public static void ShouldBe(this ItemDto? item, ItemDto expectedItem)
    {
        item.Should().NotBeNull();
        item!.Name.Should().Be(expectedItem.Name);
        item.Description.Should().Be(expectedItem.Description);
        item.BasePrice.Should().Be(expectedItem.BasePrice);
        item.BaseCurrency.Should().Be(expectedItem.BaseCurrency);
    }
}