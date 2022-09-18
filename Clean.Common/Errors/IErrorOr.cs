namespace Clean.Common.Errors;
public interface IErrorOr
{
    public IReadOnlyCollection<Error> Errors { get; }
}
