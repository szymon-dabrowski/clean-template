using Clean.Modules.Crm.Domain.Orders.Rules;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders;
public class Order : AggregateRoot<Guid>
{
    private Order(
        Guid id,
        Guid customerId,
        DateTime orderDate,
        string orderNumber,
        IEnumerable<OrderItem> orderItems)
        : base(id)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
        OrderNumber = orderNumber;
        OrderItems = orderItems.ToArray();
    }

    public Guid CustomerId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public string OrderNumber { get; private set; }

    public IReadOnlyCollection<OrderItem> OrderItems { get; private set; }

    public bool IsDeleted { get; private set; }

    internal static async Task<ErrorOr<Order>> Create(
        Guid customerId,
        DateTime orderDate,
        string? orderNumber,
        IEnumerable<OrderItem> orderItems,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker,
        IOrderNumberGenerator orderNumberGenerator)
    {
        var orderNo = orderNumber ?? await orderNumberGenerator.GenerateOrderNumber();

        var result = await Check(
            new OrderNumberMustBeUniqueRule(orderNo, orderNumberUniquenessChecker),
            new OrderMustContainUniqueOrderItemsRule(orderItems),
            new CustomerMustExistRule(customerId, customerExistenceChecker),
            new ItemsMustExistRule(
                orderItems.Select(i => i.ItemId),
                itemExistenceChecker));

        if (result.IsError)
        {
            return Error.From(result);
        }

        return new Order(
            Guid.NewGuid(),
            customerId,
            orderDate,
            orderNo,
            orderItems);
    }

    internal async Task<ErrorOr<Order>> Update(
        Guid customerId,
        DateTime orderDate,
        string orderNumber,
        IEnumerable<OrderItem> orderItems,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker)
    {
        var result = await Check(
            new CannotUpdateDeletedOrderRule(IsDeleted),
            new OrderNumberMustBeUniqueRule(orderNumber, orderNumberUniquenessChecker),
            new OrderMustContainUniqueOrderItemsRule(orderItems),
            new CustomerMustExistRule(customerId, customerExistenceChecker),
            new ItemsMustExistRule(
                orderItems.Select(i => i.ItemId),
                itemExistenceChecker));

        if (result.IsError)
        {
            return Error.From(result);
        }

        CustomerId = customerId;
        OrderDate = orderDate;
        OrderNumber = orderNumber;
        OrderItems = orderItems.ToArray();

        return this;
    }

    internal void Delete()
    {
        IsDeleted = true;
    }
}