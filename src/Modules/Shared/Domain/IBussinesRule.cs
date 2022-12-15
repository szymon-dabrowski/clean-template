namespace Clean.Modules.Shared.Domain;
public interface IBussinesRule
{
    public string Message { get; }

    public Task<bool> IsBroken();
}