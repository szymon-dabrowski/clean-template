namespace Clean.Modules.Shared.Common.Errors;
public readonly record struct Error
{
    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }
    public string TypeName => Type.ToString();

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public static Error NotFound(
        string code = "General.NotFound",
        string description = "A 'not found' error has occurred.")
        => new(code, description, ErrorType.NotFound);

    public static Error Validation(
        string code = "General.Validation",
        string description = "A validation error has occurred.")
        => new(code, description, ErrorType.Validation);

    public static Error Failure(
        string code = "General.Failure",
        string description = "A failure error has occurred.")
        => new(code, description, ErrorType.Failure);

    public static Error Conflict(
        string code = "General.Conflict",
        string description = "A conflict error has occurred.")
        => new(code, description, ErrorType.Conflict);

    public static Error Custom(
        string code,
        string description,
        ErrorType errorType)
        => new(code, description, errorType);
}
