using CardEntity = Card.Domain.Entities.Card;

namespace Card.Application.Abstractions;

public interface ICardRepository
{
    Task<CardEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<CardEntity>> GetByAccountIdAsync(Guid accountId, CancellationToken ct = default);
    Task AddAsync(CardEntity card, CancellationToken ct = default);
    Task UpdateAsync(CardEntity card, CancellationToken ct = default);
}