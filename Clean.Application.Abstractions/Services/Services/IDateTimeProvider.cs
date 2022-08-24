namespace Clean.Application.Abstractions.Services.Services;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
