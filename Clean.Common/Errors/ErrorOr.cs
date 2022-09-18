using OneOf;

namespace Clean.Common.Errors;

public readonly struct ErrorOr<TValue> : IErrorOr
{
    private readonly OneOf<TValue, IReadOnlyCollection<Error>> result;

    public IReadOnlyCollection<Error> Errors => result.AsT1;

    private ErrorOr(Error error)
    {
        result = new[] { error };
    }

    public ErrorOr(IEnumerable<Error> errors)
    {
        result = errors.ToArray();
    }

    private ErrorOr(TValue value)
    {
        result = value;
    }

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
