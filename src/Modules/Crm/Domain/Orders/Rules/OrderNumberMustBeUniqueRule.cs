using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class OrderNumberMustBeUniqueRule : IBussinesRule
{
    private readonly string orderNumber;
    private readonly IOrderNumberUniquenessChecker orderNumberUniquenessChecker;

    public OrderNumberMustBeUniqueRule(
        string orderNumber,
        IOrderNumberUniquenessChecker orderNumberUniquenessChecker)
    {
        this.orderNumber = orderNumber;
        this.orderNumberUniquenessChecker = orderNumberUniquenessChecker;
    }

    public string Message => "Order with given name already exists.";

    public async Task<bool> IsBroken() => !await orderNumberUniquenessChecker.IsUnique(orderNumber);
}