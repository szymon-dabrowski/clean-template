namespace Clean.Modules.Crm.Domain.Orders.Services;
public interface IOrderNumberUniquenessChecker
{
    Task<bool> IsUnique(string orderNumber);
}