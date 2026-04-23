using Account.Application.Abstractions;
using Account.Application.DTOs;

namespace Account.Application.Queries.GetAllAccounts;

public record GetAllAccountsQuery(Guid OwnerId) : IQuery<IEnumerable<AccountDto>>;