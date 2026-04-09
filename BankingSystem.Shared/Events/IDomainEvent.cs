using MediatR;

namespace BankingSystem.Shared.Events;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
