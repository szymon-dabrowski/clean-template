using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Events;
internal record OrderConfirmedDomainEvent(
    Guid OrderId,
    decimal Worth,
    string Currency) : DomainEventBase;