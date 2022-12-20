using OneOf;
using System.Collections.ObjectModel;

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

    public TValue Value => result.AsT0;

    public IReadOnlyCollection<Error> Errors => result.AsT1;

    public bool IsError => result.IsT1 && Errors.Any();

    public static implicit operator ErrorOr<TValue>(TValue value)
        => new(value);

    public static implicit operator ErrorOr<TValue>(Error error)
        => new(error);

    public static implicit operator ErrorOr<TValue>(List<Error> errors)
        => new(errors);

    public static implicit operator ErrorOr<TValue>(Error[] errors)
        => new(errors);

    public static implicit operator ErrorOr<TValue>(ReadOnlyCollection<Error> errors)
        => new(errors);

    public TResult Match<TResult>(
        Func<TValue, TResult> valueMatch,
        Func<IReadOnlyCollection<Error>, TResult> errorMatch)
    {
        return result.Match(valueMatch, errorMatch);
    }
}