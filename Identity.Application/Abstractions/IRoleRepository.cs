using Identity.Domain.Entities;

namespace Identity.Application.Abstractions;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name, CancellationToken ct = default);
    Task AddAsync(Role role, CancellationToken ct = default);
}