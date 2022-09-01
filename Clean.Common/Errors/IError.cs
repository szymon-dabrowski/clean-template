namespace Clean.Common.Errors;

public interface IError
{
    public string Code { get; }
    public string Description { get; }
}
