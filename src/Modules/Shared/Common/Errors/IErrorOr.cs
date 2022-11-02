namespace Clean.Modules.Shared.Common.Errors;

public interface IErrorOr
{
    public IReadOnlyCollection<Error> Errors { get; }
}
