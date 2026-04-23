using Account.Application.Abstractions;
using Account.Application.DTOs;

namespace Account.Application.Queries.GetAccount;

public record GetAccountQuery(Guid AccountId) : IQuery<AccountDto?>;