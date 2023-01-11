using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Application.Orders.Dto;
using FluentAssertions;

namespace Clean.Modules.Crm.IntegrationTests.Orders;
internal static class OrdersTestsExtensions
{
    public static OrderCustomerDetailsDto ToOrderCustomerDetials(this CustomerDto customer)
        => new(
            customer.Id,
            customer.Name,
            customer.TaxId,
            customer.Address,
            customer.PostalCode,
            customer.City,
            customer.Phones,
            customer.Emails);

    public static OrderItemDetailsDto ToOrderItemDetials(
        this ItemDto item,
        decimal quantity,
        decimal pricePerUnit)
        => new(
            item.Id,
            item.Name,
            item.Description,
            quantity,
            item.BasePrice,
            pricePerUnit,
            item.BaseCurrency);

    public static void ShouldBe(this OrderDto? order, OrderDto expectedOrder)
    {
        order.Should().NotBeNull();
        order!.Id.Should().NotBeEmpty();
        order.CustomerId.Should().Be(expectedOrder.CustomerId);
        order.OrderNumber.Should().Be(expectedOrder.OrderNumber);
        order.OrderDate.Should().Be(expectedOrder.OrderDate);
        order.Currency.Should().Be(expectedOrder.Currency);
        order.OrderItems.Should().HaveCount(expectedOrder.OrderItems.Count);
        order.OrderItems.Should().BeEquivalentTo(expectedOrder.OrderItems);
    }

    public static void ShouldBe(this OrderDetailsDto? order, OrderDetailsDto expectedOrder)
    {
        order.Should().NotBeNull();
        order!.OrderId.Should().NotBeEmpty();
        order.OrderNumber.Should().Be(expectedOrder.OrderNumber);
        order.OrderDate.Should().Be(expectedOrder.OrderDate);
        order.Currency.Should().Be(expectedOrder.Currency);

        order.Customer.ShouldBe(expectedOrder.Customer);
        order.OrderItems.ShouldBe(expectedOrder.OrderItems);
    }

    private static void ShouldBe(
        this List<OrderItemDetailsDto> orderItemsDetails,
        List<OrderItemDetailsDto> expectedOrderItemsDetails)
    {
        orderItemsDetails.Should().HaveCount(expectedOrderItemsDetails.Count);

        foreach (var orderItemDetials in orderItemsDetails)
        {
            expectedOrderItemsDetails
                .Should()
                .Contain(i => i.ItemId == orderItemDetials.ItemId);

            var expectedOrderItem = expectedOrderItemsDetails
                .First(i => i.ItemId == orderItemDetials.ItemId);

            expectedOrderItem.ShouldBe(orderItemDetials);
        }
    }

    private static void ShouldBe(
        this OrderItemDetailsDto? orderItemDetails,
        OrderItemDetailsDto expectedOrderItemDetails)
    {
        orderItemDetails.Should().NotBeNull();
        orderItemDetails!.Name.Should().Be(expectedOrderItemDetails.Name);
        orderItemDetails.Description.Should().Be(expectedOrderItemDetails.Description);
        orderItemDetails.PricePerUnit.Should().Be(expectedOrderItemDetails.PricePerUnit);
        orderItemDetails.BasePrice.Should().Be(expectedOrderItemDetails.BasePrice);
        orderItemDetails.Quantity.Should().Be(expectedOrderItemDetails.Quantity);
        orderItemDetails.BaseCurrency.Should().Be(expectedOrderItemDetails.BaseCurrency);
    }

    private static void ShouldBe(
        this OrderCustomerDetailsDto? orderCustomerDetails,
        OrderCustomerDetailsDto expectedOrderCustomerDetails)
    {
        orderCustomerDetails.Should().NotBeNull();
        orderCustomerDetails!.CustomerId.Should().Be(expectedOrderCustomerDetails.CustomerId);
        orderCustomerDetails.TaxId.Should().Be(expectedOrderCustomerDetails.TaxId);
        orderCustomerDetails.Name.Should().Be(expectedOrderCustomerDetails.Name);
        orderCustomerDetails.Address.Should().Be(expectedOrderCustomerDetails.Address);
        orderCustomerDetails.City.Should().Be(expectedOrderCustomerDetails.City);
        orderCustomerDetails.PostalCode.Should().Be(expectedOrderCustomerDetails.PostalCode);
        orderCustomerDetails.Phones.Should().HaveCount(orderCustomerDetails.Phones.Count);
        orderCustomerDetails.Phones.Should().BeEquivalentTo(orderCustomerDetails.Phones);
        orderCustomerDetails.Emails.Should().HaveCount(orderCustomerDetails.Emails.Count);
        orderCustomerDetails.Emails.Should().BeEquivalentTo(orderCustomerDetails.Emails);
    }
}