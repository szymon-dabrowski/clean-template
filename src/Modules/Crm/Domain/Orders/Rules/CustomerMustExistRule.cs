using Clean.Modules.Crm.Domain.Orders.Services;
using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Rules;
internal class CustomerMustExistRule : IBussinesRule
{
    private readonly Guid customerId;
    private readonly ICustomerExistenceChecker customerExistenceChecker;

    public CustomerMustExistRule(
        Guid customerId,
        ICustomerExistenceChecker customerExistenceChecker)
    {
        this.customerId = customerId;
        this.customerExistenceChecker = customerExistenceChecker;
    }

    public string Message => "Cannot create order for non existing customer.";

    public async Task<bool> IsBroken() => !await customerExistenceChecker.CustomerExists(customerId);
}