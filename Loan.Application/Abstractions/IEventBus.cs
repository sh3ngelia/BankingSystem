using BankingSystem.Shared.Events;

namespace Loan.Application.Abstractions;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : IDomainEvent;
}