using Clean.Modules.Shared.Domain;

namespace Clean.Modules.Crm.Domain.Orders.Events;
internal record OrderCanceledDomainEvent(Guid OrderId) : DomainEventBase;