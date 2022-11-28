using MediatR;

namespace Clean.Modules.Shared.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTime OccurredOn { get; }
}
