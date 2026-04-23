using Account.Application.Abstractions;
using Account.Domain.Enums;
using Account.Domain.ValueObjects;
using BankingSystem.Shared.Enums;
using BankingSystem.Shared.Results;
using BankingSystem.Shared.ValueObjects;

namespace Account.Application.Commands.DepositMoney;

public class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DepositMoneyCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DepositMoneyCommand command, CancellationToken ct)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, ct);
        if (account is null)
            return Result.Failure("Account not found.");

        if (!Enum.TryParse<Currency>(command.Currency, out var currency))
            return Result.Failure("Invalid currency.");

        var money = Money.Create(command.Amount, currency);
        account.Deposit(money);

        await _accountRepository.UpdateAsync(account, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}