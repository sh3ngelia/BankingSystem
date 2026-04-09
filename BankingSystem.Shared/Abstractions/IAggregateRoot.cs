using BankingSystem.Shared.Events;

namespace BankingSystem.Shared.Abstractions;

public interface IAggregateRoot : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
