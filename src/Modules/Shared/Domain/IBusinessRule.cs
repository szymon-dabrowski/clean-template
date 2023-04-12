namespace Clean.Modules.Shared.Domain;
public interface IBusinessRule
{
    public string Message { get; }

    public Task<bool> IsBroken();
}