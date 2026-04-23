using Account.Application.Abstractions;
using Account.Application.DTOs;

namespace Account.Application.Queries.GetAccount;

public class GetAccountQueryHandler : IQueryHandler<GetAccountQuery, AccountDto?>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountDto?> Handle(GetAccountQuery query, CancellationToken ct)
    {
        var account = await _accountRepository.GetByIdAsync(query.AccountId, ct);
        if (account is null) return null;

        return new AccountDto(
            account.Id,
            account.AccountNumber.Value,
            account.OwnerId,
            account.Balance.Amount,
            account.Balance.Currency.ToString(),
            account.Type.ToString(),
            account.Status.ToString(),
            account.CreatedAt);
    }
}