using OneOf;

namespace Clean.Modules.Shared.Common.Errors;

public readonly struct ErrorOr<TValue> : IErrorOr
{
    private readonly OneOf<TValue, IReadOnlyCollection<Error>> result;

    public ErrorOr(IEnumerable<Error> errors)
    {
        result = errors.ToArray();
    }

    private ErrorOr(Error error)
    {
        result = new[] { error };
    }

    private ErrorOr(TValue value)
    {
        result = value;
    }

    public IReadOnlyCollection<Error> Errors => result.AsT1;

    public static implicit operator ErrorOr<TValue>(TValue value)
    {
        return new ErrorOr<TValue>(value);
    }

    public static implicit operator ErrorOr<TValue>(Error error)
    {
        return new ErrorOr<TValue>(error);
    }

    public static implicit operator ErrorOr<TValue>(List<Error> errors)
    {
        return new ErrorOr<TValue>(errors);
    }

    public static implicit operator ErrorOr<TValue>(Error[] errors)
    {
        return new ErrorOr<TValue>(errors);
    }

    public TResult Match<TResult>(
        Func<TValue, TResult> valueMatch,
        Func<IReadOnlyCollection<Error>, TResult> errorMatch)
    {
        return result.Match(valueMatch, errorMatch);
    }
}