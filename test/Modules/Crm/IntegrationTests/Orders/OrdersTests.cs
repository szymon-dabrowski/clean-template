using Clean.Modules.Crm.Application.Customers.Dto;
using Clean.Modules.Crm.Application.Items.Dto;
using Clean.Modules.Crm.Application.Orders.CreateOrder;
using Clean.Modules.Crm.Application.Orders.DeleteOrder;
using Clean.Modules.Crm.Application.Orders.Dto;
using Clean.Modules.Crm.Application.Orders.GetOrder;
using Clean.Modules.Crm.Application.Orders.GetOrderDetails;
using Clean.Modules.Crm.Application.Orders.GetOrders;
using Clean.Modules.Crm.Application.Orders.GetOrdersDetails;
using Clean.Modules.Crm.Application.Orders.UpdateOrder;
using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.IntegrationTests.SeedWork;
using FluentAssertions;
using Xunit;

namespace Clean.Modules.Crm.IntegrationTests.Orders;
public class OrdersTests : IClassFixture<CrmStartupFixture>, IClassFixture<OrdersTestsFixture>
{
    private readonly CrmModule crmModule;
    private readonly CustomerDto customer;
    private readonly ItemDto item1;
    private readonly ItemDto item2;

    public OrdersTests(CrmStartupFixture crmStartupFixture, OrdersTestsFixture ordersTestsFixture)
    {
        crmModule = crmStartupFixture.CrmModule;

        ordersTestsFixture
            .SeedWork(crmStartupFixture.CrmModule)
            .GetAwaiter()
            .GetResult();

        customer = ordersTestsFixture.Customer;
        item1 = ordersTestsFixture.Item1;
        item2 = ordersTestsFixture.Item2;
    }

    [Fact]
    public async Task CreateOrder_OrderDataValid_OrderCreated()
    {
        var orderItem1 = new OrderItemDto(item1.Id, Quantity: 10, PricePerUnit: 100);
        var orderItem2 = new OrderItemDto(item2.Id, Quantity: 20, PricePerUnit: 200);

        var expectedOrder = new OrderDto(
            Id: Guid.Empty,
            CustomerId: customer.Id,
            OrderNumber: "Order Number",
            OrderDate: DateTime.UtcNow,
            Currency: "EUR",
            OrderItems: new() { orderItem1, orderItem2 });

        var result = await crmModule.ExecuteCommand(new CreateOrderCommand(
            expectedOrder.CustomerId,
            expectedOrder.OrderNumber,
            expectedOrder.OrderDate,
            expectedOrder.Currency,
            expectedOrder.OrderItems));

        var orders = await crmModule.ExecuteQuery(new GetOrdersQuery());
        var order = orders.First(o => o.Id == result.Value);

        result.IsError.Should().BeFalse();
        order.ShouldBe(expectedOrder);
    }

    [Fact]
    public async Task UpdateOrder_ValidOrderData_OrderUpdated()
    {
        var orderItem1 = new OrderItemDto(item1.Id, Quantity: 10, PricePerUnit: 100);
        var orderItem2 = new OrderItemDto(item2.Id, Quantity: 20, PricePerUnit: 200);

        var result = await crmModule.ExecuteCommand(new CreateOrderCommand(
            CustomerId: customer.Id,
            OrderNumber: "Order To Update Number",
            OrderDate: new DateTime(2022, 10, 10),
            Currency: "EUR",
            OrderItems: new() { orderItem1, orderItem2 }));

        var updatedOrderItem1 = new OrderItemDto(item1.Id, Quantity: 11, PricePerUnit: 101);

        var expectedOrder = new OrderDto(
            Id: result.Value,
            CustomerId: customer.Id,
            OrderNumber: "Updated Order Number",
            OrderDate: new DateTime(2023, 12, 12),
            Currency: "USD",
            OrderItems: new() { updatedOrderItem1 });

        var updateResult = await crmModule.ExecuteCommand(new UpdateOrderCommand(
            expectedOrder.Id,
            expectedOrder.CustomerId,
            expectedOrder.OrderNumber,
            expectedOrder.OrderDate,
            expectedOrder.Currency,
            expectedOrder.OrderItems));

        var order = await crmModule.ExecuteQuery(new GetOrderQuery(result.Value));

        updateResult.IsError.Should().BeFalse();
        order.ShouldBe(expectedOrder);
    }

    [Fact]
    public async Task DeleteOrder_OrderExists_OrderDeleted()
    {
        var orderItem1 = new OrderItemDto(item1.Id, Quantity: 10, PricePerUnit: 100);

        var result = await crmModule.ExecuteCommand(new CreateOrderCommand(
            CustomerId: customer.Id,
            OrderNumber: "Order To Delete Number",
            OrderDate: new DateTime(2022, 10, 10),
            Currency: "EUR",
            OrderItems: new() { orderItem1 }));

        await crmModule.ExecuteCommand(new DeleteOrderCommand(result.Value));

        var order = await crmModule.ExecuteQuery(new GetOrderQuery(result.Value));

        order.Should().BeNull();
    }

    [Fact]
    public async Task GetOrderDetials_OrderExists_ReturnsOrderDetails()
    {
        var orderItem1 = new OrderItemDto(item1.Id, Quantity: 11, PricePerUnit: 101);
        var orderItem2 = new OrderItemDto(item2.Id, Quantity: 22, PricePerUnit: 202);

        var orderItemDetails1 = item1.ToOrderItemDetials(
            quantity: orderItem1.Quantity,
            pricePerUnit: orderItem1.PricePerUnit);

        var orderItemDetails2 = item2.ToOrderItemDetials(
            quantity: orderItem2.Quantity,
            pricePerUnit: orderItem2.PricePerUnit);

        var orderCustomerDetails = customer.ToOrderCustomerDetials();

        var expectedOrderDetails = new OrderDetailsDto(
            OrderId: Guid.Empty,
            OrderNumber: "Order Details Number",
            OrderDate: DateTime.UtcNow,
            Currency: "EUR",
            Customer: orderCustomerDetails,
            OrderItems: new() { orderItemDetails1, orderItemDetails2 });

        var result = await crmModule.ExecuteCommand(new CreateOrderCommand(
            expectedOrderDetails.Customer.CustomerId,
            expectedOrderDetails.OrderNumber,
            expectedOrderDetails.OrderDate,
            expectedOrderDetails.Currency,
            new() { orderItem1, orderItem2 }));

        var orderDetails = await crmModule.ExecuteQuery(new GetOrderDetailsQuery(result.Value));

        orderDetails.ShouldBe(expectedOrderDetails);
    }

    [Fact]
    public async Task GetOrdersDetials_OrdersExists_ReturnsOrdersDetails()
    {
        var orderItem1 = new OrderItemDto(item1.Id, Quantity: 11, PricePerUnit: 101);
        var orderItem2 = new OrderItemDto(item2.Id, Quantity: 22, PricePerUnit: 202);

        var orderItemDetails1 = item1.ToOrderItemDetials(
            quantity: orderItem1.Quantity,
            pricePerUnit: orderItem1.PricePerUnit);

        var orderItemDetails2 = item2.ToOrderItemDetials(
            quantity: orderItem2.Quantity,
            pricePerUnit: orderItem2.PricePerUnit);

        var orderCustomerDetails = customer.ToOrderCustomerDetials();

        var expectedOrderDetails1 = new OrderDetailsDto(
            OrderId: Guid.Empty,
            OrderNumber: "Order Details Number 1",
            OrderDate: DateTime.UtcNow,
            Currency: "EUR",
            Customer: orderCustomerDetails,
            OrderItems: new() { orderItemDetails1, orderItemDetails2 });

        var expectedOrderDetails2 = new OrderDetailsDto(
            OrderId: Guid.Empty,
            OrderNumber: "Order Details Number 2",
            OrderDate: DateTime.UtcNow,
            Currency: "EUR",
            Customer: orderCustomerDetails,
            OrderItems: new() { orderItemDetails1 });

        var result1 = await crmModule.ExecuteCommand(new CreateOrderCommand(
            expectedOrderDetails1.Customer.CustomerId,
            expectedOrderDetails1.OrderNumber,
            expectedOrderDetails1.OrderDate,
            expectedOrderDetails1.Currency,
            new() { orderItem1, orderItem2 }));

        var result2 = await crmModule.ExecuteCommand(new CreateOrderCommand(
            expectedOrderDetails2.Customer.CustomerId,
            expectedOrderDetails2.OrderNumber,
            expectedOrderDetails2.OrderDate,
            expectedOrderDetails2.Currency,
            new() { orderItem1 }));

        var ordersDetails = await crmModule.ExecuteQuery(
            new GetOrdersDetailsQuery(new[] { result1.Value, result2.Value }));

        ordersDetails = ordersDetails
            .OrderBy(x => x.OrderNumber)
            .ToList();

        ordersDetails.Should().HaveCount(2);
        ordersDetails[0].ShouldBe(expectedOrderDetails1);
        ordersDetails[1].ShouldBe(expectedOrderDetails2);
    }
}