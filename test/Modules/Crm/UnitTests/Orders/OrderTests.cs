using Clean.Modules.Crm.Domain.Money;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Common.Errors;
using FluentAssertions;
using Moq;
using Xunit;

namespace Clean.Modules.Crm.UnitTests.Orders;
public class OrderTests
{
    [Fact]
    public async Task CreatingOrder_WhenOrderNumberIsUnique_ShouldReturnCreatedOrder()
    {
        var orderItem1id = Guid.NewGuid();
        var orderItem1 = OrderItem.Create(
            orderItem1id,
            quanity: 100,
            PriceValueObject.Create(value: 100, currency: "eur"));

        var orderItem2id = Guid.NewGuid();
        var orderItem2 = OrderItem.Create(
            orderItem2id,
            quanity: 200,
            PriceValueObject.Create(value: 200, currency: "eur"));

        var customerId = Guid.NewGuid();
        var orderDate = DateTime.Now;
        var orderNumber = "testOrder123";
        var orderItems = new OrderItem[]
        {
            orderItem1,
            orderItem2,
        };

        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: orderNumber);

        var order = await Order.Create(
            customerId,
            orderDate,
            orderNumber,
            orderItems,
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        order.IsError.Should().BeFalse();
        order.Value.OrderNumber.Should().Be(orderNumber);
        order.Value.OrderDate.Should().Be(orderDate);
        order.Value.CustomerId.Should().Be(customerId);
        order.Value.OrderItems.Should().HaveCount(orderItems.Length);
        AssertOrderItem(order.Value.OrderItems.Single(i => i.ItemId == orderItem1id), orderItem1);
        AssertOrderItem(order.Value.OrderItems.Single(i => i.ItemId == orderItem2id), orderItem2);
    }

    [Fact]
    public async Task CreatingOrder_WhenOrderNumberIsNotUnique_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: false);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        order.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreatingOrder_WhenAnyOrderItemIsNotUnique_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var orderItem = OrderItem.Create(
            itemId: Guid.NewGuid(),
            quanity: 100,
            PriceValueObject.Create(value: 100, currency: "eur"));

        var orderItems = new OrderItem[]
        {
            orderItem,
            orderItem,
        };

        var order = await Order.Create(
            customerId: Guid.NewGuid(),
            orderDate: DateTime.Now,
            orderNumber: "TestOrderNumber",
            orderItems,
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        order.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreatingOrder_WhenCustomerNotExists_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: false);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        order.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreatingOrder_WhenAnyOrderItemNotExists_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: false);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        order.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreatingOrder_WithEmptyOrderNumber_ShouldReturnOrderWithGeneratedNumber()
    {
        var generatedOrderNumber = "generatedOrder";
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: generatedOrderNumber);

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator,
            orderNumber: null);

        order.Value.OrderNumber.Should().Be(generatedOrderNumber);
    }

    [Fact]
    public async Task UpdatingOrder_WhenOrderNumberIsUnique_ShouldReturnUpdatedOrder()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrderNumber");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        var orderItem1id = Guid.NewGuid();
        var orderItem1 = OrderItem.Create(
            orderItem1id,
            quanity: 100,
            PriceValueObject.Create(value: 100, currency: "eur"));

        var orderItem2id = Guid.NewGuid();
        var orderItem2 = OrderItem.Create(
            orderItem2id,
            quanity: 200,
            PriceValueObject.Create(value: 200, currency: "eur"));

        var customerId = Guid.NewGuid();
        var orderDate = new DateTime(2022, 12, 15);
        var orderNumber = "testOrder123";
        var orderItems = new OrderItem[]
        {
            orderItem1,
            orderItem2,
        };

        var updateResult = await order.Value.Update(
            customerId,
            orderDate,
            orderNumber: orderNumber,
            orderItems,
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker);

        updateResult.IsError.Should().BeFalse();
        updateResult.Value.OrderNumber.Should().Be(orderNumber);
        updateResult.Value.OrderDate.Should().Be(orderDate);
        updateResult.Value.CustomerId.Should().Be(customerId);
        updateResult.Value.OrderItems.Should().HaveCount(orderItems.Length);
        AssertOrderItem(updateResult.Value.OrderItems.Single(i => i.ItemId == orderItem1id), orderItem1);
        AssertOrderItem(updateResult.Value.OrderItems.Single(i => i.ItemId == orderItem2id), orderItem2);
    }

    [Fact]
    public async Task UpdatingOrder_WhenOrderNumberIsNotUnique_ShouldReturnError()
    {
        var successOrderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var failingOrderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: false);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            successOrderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        var updateResult = await order.Value.Update(
            order.Value.CustomerId,
            order.Value.OrderDate,
            order.Value.OrderNumber,
            order.Value.OrderItems,
            failingOrderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingOrder_WhenAnyOrderItemIsNotUnique_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        var orderItem = OrderItem.Create(
            itemId: Guid.NewGuid(),
            quanity: 100,
            PriceValueObject.Create(value: 100, currency: "eur"));

        var orderItems = new OrderItem[]
        {
            orderItem,
            orderItem,
        };

        var updateResult = await order.Value.Update(
            order.Value.CustomerId,
            order.Value.OrderDate,
            order.Value.OrderNumber,
            orderItems,
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingOrder_WhenOrderIsDeleted_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        order.Value.Delete();

        var updateResult = await order.Value.Update(
            order.Value.CustomerId,
            order.Value.OrderDate,
            order.Value.OrderNumber,
            order.Value.OrderItems,
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingOrder_WhenCustomerNotExists_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var successCustomerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var failingCustomerExistenceChecker = MockCustomerExistenceChecker(withResult: false);
        var itemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            successCustomerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        var updateResult = await order.Value.Update(
            order.Value.CustomerId,
            order.Value.OrderDate,
            order.Value.OrderNumber,
            order.Value.OrderItems,
            orderNumberUniquenessChecker,
            failingCustomerExistenceChecker,
            itemExistenceChecker);

        updateResult.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task UpdatingOrder_WhenAnyOrderItemNotExists_ShouldReturnError()
    {
        var orderNumberUniquenessChecker = MockOrderNameUniquenessChecker(withResult: true);
        var customerExistenceChecker = MockCustomerExistenceChecker(withResult: true);
        var successItemExistenceChecker = MockItemExistenceChecker(withResult: true);
        var failingItemExistenceChecker = MockItemExistenceChecker(withResult: false);
        var orderNumberGenerator = MockOrderNumberGenerator(withResult: "generatedOrder");

        var order = await CreateTestOrder(
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            successItemExistenceChecker,
            orderNumberGenerator);

        var updateResult = await order.Value.Update(
            order.Value.CustomerId,
            order.Value.OrderDate,
            order.Value.OrderNumber,
            order.Value.OrderItems,
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            failingItemExistenceChecker);

        updateResult.IsError.Should().BeTrue();
    }

    private ICustomerExistenceChecker MockCustomerExistenceChecker(bool withResult)
    {
        var customerExistenceChecker = new Mock<ICustomerExistenceChecker>();

        customerExistenceChecker
            .Setup(c => c.CustomerExists(It.IsAny<Guid>()))
            .Returns<Guid>(_ => Task.FromResult(withResult));

        return customerExistenceChecker.Object;
    }

    private IItemExistenceChecker MockItemExistenceChecker(bool withResult)
    {
        var itemExistenceChecker = new Mock<IItemExistenceChecker>();

        itemExistenceChecker
            .Setup(c => c.ItemsExists(It.IsAny<IEnumerable<Guid>>()))
            .Returns<IEnumerable<Guid>>(_ => Task.FromResult(withResult));

        return itemExistenceChecker.Object;
    }

    private IOrderNumberGenerator MockOrderNumberGenerator(string withResult)
    {
        var orderNumberGenerator = new Mock<IOrderNumberGenerator>();

        orderNumberGenerator
            .Setup(c => c.GenerateOrderNumber())
            .Returns(Task.FromResult(withResult));

        return orderNumberGenerator.Object;
    }

    private IOrderNumberUniquenessChecker MockOrderNameUniquenessChecker(bool withResult)
    {
        var orderNumberUniquenessChecker = new Mock<IOrderNumberUniquenessChecker>();

        orderNumberUniquenessChecker
            .Setup(c => c.IsUnique(It.IsAny<string>()))
            .Returns<string>(_ => Task.FromResult(withResult));

        return orderNumberUniquenessChecker.Object;
    }

    private async Task<ErrorOr<Order>> CreateTestOrder(
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker,
        IOrderNumberGenerator orderNumberGenerator,
        string? orderNumber = "test")
    {
        var pricePerUnit = PriceValueObject.Create(value: 100, currency: "eur");

        var orderItem = OrderItem.Create(
            itemId: Guid.NewGuid(),
            quanity: 10,
            pricePerUnit);

        var order = await Order.Create(
            customerId: Guid.NewGuid(),
            orderDate: DateTime.Now,
            orderNumber,
            orderItems: new OrderItem[] { orderItem },
            orderNumberUniquenessChecker,
            customerExistenceChecker,
            itemExistenceChecker,
            orderNumberGenerator);

        return order;
    }

    private void AssertOrderItem(OrderItem expected, OrderItem actual)
    {
        actual.Quantity.Should().Be(expected.Quantity);
        actual.PricePerUnit.Currency.Should().Be(expected.PricePerUnit.Currency);
        actual.PricePerUnit.Value.Should().Be(expected.PricePerUnit.Value);
    }
}