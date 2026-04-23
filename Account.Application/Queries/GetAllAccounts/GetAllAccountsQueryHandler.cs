using Account.Application.Abstractions;
using Account.Application.DTOs;

namespace Account.Application.Queries.GetAllAccounts;

public class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAllAccountsQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<AccountDto>> Handle(GetAllAccountsQuery query, CancellationToken ct)
    {
        var accounts = await _accountRepository.GetByOwnerIdAsync(query.OwnerId, ct);

        return accounts.Select(account => new AccountDto(
            account.Id,
            account.AccountNumber.Value,
            account.OwnerId,
            account.Balance.Amount,
            account.Balance.Currency.ToString(),
            account.Type.ToString(),
            account.Status.ToString(),
            account.CreatedAt));
    }
}