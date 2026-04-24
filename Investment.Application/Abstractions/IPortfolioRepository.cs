using Investment.Domain.Entities;

namespace Investment.Application.Abstractions;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Portfolio>> GetByOwnerIdAsync(Guid ownerId, CancellationToken ct = default);
    Task AddAsync(Portfolio portfolio, CancellationToken ct = default);
    Task UpdateAsync(Portfolio portfolio, CancellationToken ct = default);
}