using Account.Application.Abstractions;
using BankingSystem.Shared.Results;

namespace Account.Application.Commands.UnfreezeAccount;

public class UnfreezeAccountCommandHandler : ICommandHandler<UnfreezeAccountCommand>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UnfreezeAccountCommandHandler(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UnfreezeAccountCommand command, CancellationToken ct)
    {
        var account = await _accountRepository.GetByIdAsync(command.AccountId, ct);
        if (account is null)
            return Result.Failure("Account not found.");

        account.Unfreeze();

        await _accountRepository.UpdateAsync(account, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}