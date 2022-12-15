namespace Clean.Modules.Crm.Domain.Orders.Services;
public interface IOrderNumberGenerator
{
    Task<string> GenerateOrderNumber();
}