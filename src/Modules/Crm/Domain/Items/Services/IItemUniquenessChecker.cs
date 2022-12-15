namespace Clean.Modules.Crm.Domain.Items.Services;
public interface IItemUniquenessChecker
{
    Task<bool> IsUnique(string name);
}