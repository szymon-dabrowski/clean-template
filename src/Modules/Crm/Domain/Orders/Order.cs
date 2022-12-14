using Clean.Modules.Crm.Domain.Orders.Rules;
using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders;
public class Order : AggregateRoot<Guid>
{
    private Order()
        : base(Guid.Empty)
    {
    }

    private Order(
        Guid id,
        Guid customerId,
        DateTime orderDate,
        string orderNumber,
        string currency,
        List<OrderItem> orderItems)
        : base(id)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
        OrderNumber = orderNumber;
        Currency = currency;
        OrderItems = orderItems;
    }

    public Guid CustomerId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public string OrderNumber { get; private set; } = string.Empty;

    public IReadOnlyCollection<OrderItem> OrderItems { get; private set; }
        = new List<OrderItem>();

    public string Currency { get; private set; } = string.Empty;

    public bool IsDeleted { get; private set; }

    public static async Task<ErrorOr<Order>> Create(
        Guid customerId,
        DateTime orderDate,
        string orderNumber,
        string currency,
        List<OrderItem> orderItems,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker,
        IOrderNumberGenerator orderNumberGenerator)
    {
        var orderNo = string.IsNullOrEmpty(orderNumber)
            ? await orderNumberGenerator.GenerateOrderNumber()
            : orderNumber;

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
            currency,
            orderItems);
    }

    public async Task<ErrorOr<Order>> Update(
        Guid customerId,
        DateTime orderDate,
        string orderNumber,
        string currency,
        List<OrderItem> orderItems,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker,
        ICustomerExistenceChecker customerExistenceChecker,
        IItemExistenceChecker itemExistenceChecker)
    {
        var rulesToCheck = new List<IBussinesRule>()
        {
            new CannotUpdateDeletedOrderRule(IsDeleted),
            new OrderMustContainUniqueOrderItemsRule(orderItems),
            new CustomerMustExistRule(customerId, customerExistenceChecker),
            new ItemsMustExistRule(
                orderItems.Select(i => i.ItemId),
                itemExistenceChecker),
        };

        if (OrderNumber != orderNumber)
        {
            rulesToCheck.Add(new OrderNumberMustBeUniqueRule(
                orderNumber,
                orderNumberUniquenessChecker));
        }

        var result = await Check(rulesToCheck.ToArray());

        if (result.IsError)
        {
            return Error.From(result);
        }

        CustomerId = customerId;
        OrderDate = orderDate;
        OrderNumber = orderNumber;
        OrderItems = orderItems;
        Currency = currency;

        return this;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}