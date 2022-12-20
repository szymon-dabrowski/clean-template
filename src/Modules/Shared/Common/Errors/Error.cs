namespace Clean.Modules.Shared.Common.Errors;
public readonly record struct Error
{
    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }

    public string Description { get; }

    public ErrorType Type { get; }

    public string TypeName => Type.ToString();

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

    public static Error BusinessRuleBroken(
        string code = "General.BussinesRuleBroken",
        string description = "A business rule broken error has occured.")
        => new(code, description, ErrorType.BusinessRuleBroken);

    public static Error EntityNotFound(
        Guid entityId,
        string code = "General.NotFound",
        string description = "Entity with Id {0} not found.")
        => new(code, string.Format(description, entityId), ErrorType.NotFound);

    public static Error Custom(
        string code,
        string description,
        ErrorType errorType)
        => new(code, description, errorType);

    public static Error From(ErrorOr<bool> result)
        => result.Errors
            .Select(e => new Error(e.Code, e.Description, e.Type))
            .First();
}